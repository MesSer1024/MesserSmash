using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Arenas;
using MesserSmash.GUI;
using MesserSmash.Enemies;
using MesserSmash.Commands;
using MesserSmash.Modules;
using SharedSmashResources.Patterns;
using SharedSmashResources;

namespace MesserSmash {
	public class SmashTVSystem : IObserver {
		private static SmashTVSystem _instance;
		public static SmashTVSystem Instance { get { return _instance; } }

		private Arena _arena;

		public Arena Arena {
			get { return _arena; }
		}
		private Player _player;

		public Player Player {
			get { return _player; }
		}
		private ShotContainer _shotContainer;

		public ShotContainer ShotContainer {
			get { return _shotContainer; }
		}

		private EnemyContainer _enemyContainer;
		public EnemyContainer EnemyContainer {
			get { return _enemyContainer; }
		}

		private EnergySystem _energySystem;
		internal EnergySystem EnergySystem {
			get { return _energySystem; }
		}
		internal GUIMain Gui {
			get { return _gui; }
		}

        public int KillCount { get { return _killCount; } }

		public SmashTVSystem() {
			_instance = this;
			Controller.instance.addObserver(this);
		}

        public string Username { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }

		private int _killCount;
		private bool _gameStarted = false;
		private List<string> _queuedCommands = new List<string>();
		private GUIMain _gui;
		private int _enemyDestructors;
		private int _behaviourDestructors;
		private int _behaviourConstructors;
		private int _enemyConstructors;
        private bool _replay;

		public void initLevel(Arena arena, Player player, ShotContainer shotContainer, EnemyContainer enemyContainer, bool replay) {
            _replay = replay;
			_gameStarted = false;
			_queuedCommands = new List<string>();
			_arena = arena;
			_player = player;
			_shotContainer = shotContainer;
			_enemyContainer = enemyContainer;
			_energySystem = EnergySystem.Instance;

			_arena.onGameFinished += new Arena.ArenaDelegate(onArenaFinished);
			_arena.onZeroTimer += new Arena.ArenaDelegate(onArenaTimerZero);
			_gui = new GUIMain();
			_gui.showLoadingScreen();
		}

		public void startLoadedLevel() {
			_gameStarted = true;
			_gui.setScore(Scoring.getTotalScore());
			_killCount = 0;
			_energySystem.reset();
			_arena.begin();
		}

		void onArenaTimerZero(Arena arena) {
			_enemyContainer.endGame();
			_shotContainer.endGame();
			_player.stateEndGame();
			_energySystem.endGame();
			Scoring.setKillsOnLevel(_killCount);
		}

		void onArenaFinished(Arena arena) {
			_arena.onGameFinished -= onArenaFinished;
            new RegisterLevelHighscoreCommand(SmashTVSystem.Instance.Username, arena.Level);
			_queuedCommands.Add("end_arena");
		}

		void onEnemyAttackedPlayer(ICommand command) {
			var cmd = command as AttackPlayerCommand;
			_player.takeDamage(cmd.Enemy.Damage);
		}

		void onEnemyDead(ICommand command) {
			var cmd = command as DeadEnemyCommand;
			Scoring.addScore(cmd.Enemy.Score);
			_arena.handleDeadEnemy(cmd.Enemy.Position, _killCount);
			++_killCount;
			_gui.setKillCount(_killCount);
			_gui.setScore(Scoring.getTotalScore());
		}

		void onPlayerDead(ICommand command) {
			var cmd = command as PlayerDiedCommand;
			_queuedCommands.Add("end_arena");

			Scoring.setKillsOnLevel(_killCount);
            if (!_replay) {
                new RegisterHighscoreCommand(SmashTVSystem.Instance.Username).execute();
                _gui.showGameOver();
            }
		}

		public void update(GameState state) {
			if (_queuedCommands.Count > 0) {
				foreach (string s in _queuedCommands) {
					if (s == "end_arena") {
						_arena.clean();
						_player = null;
						_shotContainer = null;
						_enemyContainer = null;
						_gameStarted = false;
					}
				}
				_queuedCommands.Clear();
			}
			if (_gameStarted) {
                //var state = new GameState(deltatime, _arena.TimeSinceStart);
				DataDefines.ID_STATE_ENEMIES_ALIVE = SmashTVSystem.Instance.EnemyContainer.getAliveEnemies().Count;
				DataDefines.ID_STATE_ENEMIES_KILLED = _killCount;
				_energySystem.update(state.DeltaTime);
                _enemyContainer.update(state.DeltaTime); //let all enemies decide where they want to move
                _shotContainer.update(state.DeltaTime);
				_arena.update(state);
                _player.update(state.DeltaTime);

				//check for collisions between shots and enemies
				collisionDetection();

				//check for possible enemy attacks
				checkForNearnessToPlayer();

				//check if player can pickup loot
				detectLootPickup();

				_updateGUIValues();
                _gui.update(state.DeltaTime);
			} else {
				//not started or game over
				if (_gui != null) {
                    _gui.update(state.DeltaTime);
				}
			}
		}

		private void detectLootPickup() {
			List<Loot> activeLoot = _arena.getActiveLoot();
			foreach (var loot in activeLoot) {
				Vector2 lootPos = loot.Position;
				float lootRadius = loot.Radius;
				if (isOverlapping(lootPos, _player.Position, lootRadius, _player.Radius)) {
					loot.inactivate();
					Scoring.onLoot(loot);
					if (loot.Type == Arenas.Arena.LootType.Health) {
						_player.receiveHealth(45);
					}
					_gui.setScore(Scoring.getTotalScore());
				}
			}
		}

		private void _updateGUIValues() {
			InfoWindow._energy = Utils.makeString("Energy : [{0}/{1}]", _energySystem.AvailableEnergy.ToString("000"), _energySystem.MaxEnergy.ToString("000"));
			InfoWindow._playerPosition = Utils.makeString("({0},{1})", _player.Position.X.ToString("0.0"), _player.Position.Y.ToString("0.0"));
			InfoWindow._health = _player.Health.ToString();
			InfoWindow._enemyDestructors = _enemyConstructors.ToString() + ":" + _enemyDestructors.ToString();
			InfoWindow._behaviourDestructors = _behaviourConstructors.ToString() + ":" + _behaviourDestructors.ToString();
            InfoWindow._randomStatus = Utils.makeString("Randomizer: {0}", MesserRandom.getStatus());
			_gui.setPlayerEnergy(Convert.ToInt32(_energySystem.AvailableEnergy));
			_gui.setPlayerHealth(Convert.ToInt32(_player.Health));
			_gui.setSecondsLeft(_arena.SecondsToFinish);
		}

		private void collisionDetection() {
			detectCollisionBetweenShotsAndEnemies();
			detectCollisionBetweenEnemyShotsAndPlayer();
		}

		private void detectCollisionBetweenShotsAndEnemies() {
			List<ShotBase> activeShots = _shotContainer.shotsFlaggedForCollision();
			List<IEnemy> activeEnemies = _enemyContainer.getAliveEnemies();

			foreach (ShotBase shot in activeShots) {
				Vector2 shotPos = shot.Position;
				float shotRadius = shot.Radius;
				foreach (IEnemy enemy in activeEnemies) {
					if (enemy.IsAlive) {
						if (isOverlapping(shotPos, enemy.Position, shotRadius, enemy.Radius)) {
							enemy.takeDamage(shot.Damage);
							shot.entityCollision(enemy.Position);
							if (shot.CollisionEnabled == false) {
								break;
							}
						}
					}
				}
			}            
		}

		private void detectCollisionBetweenEnemyShotsAndPlayer() {
			List<ShotBase> enemyShots = _shotContainer.enemyShotsFlaggedForCollision();

			foreach (ShotBase shot in enemyShots) {
				Vector2 shotPos = shot.Position;
				float shotRadius = shot.Radius;
				if (isOverlapping(shotPos, Player.Position, shotRadius, Player.Radius)) {
					Player.takeDamage(shot.Damage);
					shot.entityCollision(Player.Position);
				}
			}               
		}

		private bool isOverlapping(Vector2 p1, Vector2 p2, float r1, float r2) {
			float distance = (r1 + r2) * (r1 + r2);
			float dx = (p1.X - p2.X);
			float dy = (p1.Y - p2.Y);

			if (distance > (dx * dx) + (dy * dy)) {
				return true;
			}
			return false;
		}

		private void checkForNearnessToPlayer() {
			List<IEnemy> activeEnemies = _enemyContainer.getEngagingEnemies();

			foreach (var enemy in activeEnemies) {
				Vector2 enemyPos = enemy.Position;
				float enemyRadius = enemy.AttackRadius;
				if (isOverlapping(enemyPos, _player.Position, enemyRadius, Player.Radius)) {
					enemy.onPlayerInAttackRadius();
				}
			}
		}

		public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
			if (_gameStarted) {
				InfoWindow._killCount = _killCount.ToString();
				
				_arena.drawBackground(sb);
				_shotContainer.drawExplosions(sb);
				_enemyContainer.drawBegin(sb);
				_arena.drawLoot(sb);
				_enemyContainer.draw(sb);
				_player.draw(sb);
				_shotContainer.draw(sb);
				_gui.draw(sb);
			} else {
				//not started or game over
				if (_gui != null) {
					_gui.draw(sb);
				}
			}
		}

		public void enemyRemoved() {
			_enemyDestructors++;
		}

		public void enemyCreated() {
			_enemyConstructors++;
		}

		public void behaviourRemoved() {
			_behaviourDestructors++;
		}

		public void behaviourCreated() {
			_behaviourConstructors++;
		}

		public void handleCommand(ICommand cmd) {
			switch(cmd.Name) {
				case AttackPlayerCommand.NAME:
					onEnemyAttackedPlayer(cmd);
					break;
				case DeadEnemyCommand.NAME:
					onEnemyDead(cmd);
					break;
				case PlayerDiedCommand.NAME:
					onPlayerDead(cmd);
					break;
			}
		}
	}
}

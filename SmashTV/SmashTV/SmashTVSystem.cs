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
using System.ComponentModel;
using System.Threading;

namespace MesserSmash {
	public class SmashTVSystem : IObserver {
		private static SmashTVSystem _instance;
        public static SmashTVSystem Instance {
            get { return _instance; }
        }

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

        private HighscoreContainer _globalHighscores;
        public HighscoreContainer GlobalHighscores {
            get { return _globalHighscores; }
        }

        private Dictionary<int, int> _flaggedShotEnemyCollisions;
        public static bool IsGameStarted { get { return Instance._gameStarted; } }

		public SmashTVSystem() {
			_instance = this;
            Username = "";
            GameId = "";
            UserId = "";
            ServerIp = "";
            ReplayPath = "";
            GameVersion = "";
            LoginResponseKey = "";
            SessionId = "";
			Controller.instance.addObserver(this);
            _globalHighscores = new HighscoreContainer();
		}

        public uint RoundId { get; set; }
        public bool WaitingForGameCredentials { get; set; }
        public string Username { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string ServerIp { get; set; }
        public string ReplayPath { get; set; }
        public string GameVersion { get; set; }
        public string LoginResponseKey { get; set; }
        private string _sessionId;
        public string SessionId {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
        public int KillCount { get { return _killCount; } }

		private int _killCount;
		private bool _gameStarted = false;
		private List<string> _queuedCommands = new List<string>();
		private GUIMain _gui = new GUIMain();
		private int _enemyDestructors;
		private int _behaviourDestructors;
		private int _behaviourConstructors;
		private int _enemyConstructors;
        private bool _replay;
        private System.Timers.Timer _timer;

        internal void resetStates() {
            _gameStarted = false;
            if (_timer != null) {
                _timer.Stop();
                _timer.Dispose();
            }
        }

		public void initLevel(Arena arena, Player player, ShotContainer shotContainer, EnemyContainer enemyContainer, bool replay, bool restartGame) {
            _flaggedShotEnemyCollisions = new Dictionary<int, int>();
            _replay = replay;
			_gameStarted = false;
			_queuedCommands = new List<string>();
			_arena = arena;
			_player = player;
			_shotContainer = shotContainer;
			_enemyContainer = enemyContainer;
			_energySystem = EnergySystem.Instance;
            
			_arena.onArenaEnding += new Arena.ArenaDelegate(onArenaTimerZero);
            if (restartGame) {
                _gui.reset();
                _gui.showLoadingScreen();
            }
		}

		public void startLoadedLevel() {
            onManualTimer(null, null);
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(onManualTimer);
            _timer.Interval = 12500;
            _timer.Start();

            _shotContainer.clear();
            _enemyContainer.clear();
            _gui.startLevel();
			_gameStarted = true;
			_gui.setScore(Scoring.getTotalScore());
			_killCount = 0;
			_energySystem.reset();
			_arena.begin();
		}

        void onManualTimer(object sender, System.Timers.ElapsedEventArgs e) {
            ThreadWatcher.runBgThread(() => {
                new RequestRoundHighscoresCommand(SmashTVSystem.Instance.RoundId, SmashTVSystem.Instance.GlobalHighscores, null).execute();
            });
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

        void onArenaTimerZero(Arena arena) {
            _enemyContainer.endGame();
            _shotContainer.endGame();
            _player.stateEndGame();
            _energySystem.endGame();
            Scoring.setKillsOnLevel(_killCount);
        }

        public bool lastLevelInSet(int level, uint RoundId) {
            if (Utils.anyEquals(level, 10,20,100))
                return true;
            return false;
        }

        public void unloadArena() {
            _queuedCommands.Add("end_arena");
        }

		void onPlayerDead(ICommand command) {
            new RequestRoundHighscoresCommand(RoundId, GlobalHighscores, onRequestRoundHighscoreGameLost).execute();
            _gui.showGameOver(GlobalHighscores, false, (int)Scoring.getLevelScore());
            var cmd = command as PlayerDiedCommand;
			_queuedCommands.Add("end_arena");            
		}

        private void onRequestRoundHighscoreGameLost(RequestRoundHighscoresCommand cmd) {
            if (!IsGameStarted) {
                _gui.showGameOver(GlobalHighscores, true, (int)Scoring.getLevelScore());
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
				checkIfEngagingEnemiesCanBeginAttack();

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
                    new PlaySoundCommand(AssetManager.getMoneyPickupSound()).execute();
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
			_gui.setPlayerEnergy(_energySystem.AvailableEnergy, _energySystem.MaxEnergy);
			_gui.setPlayerHealth(_player.Health, 100);
			_gui.setSecondsLeft(_arena.SecondsToFinish);
		}

		private void collisionDetection() {
            _flaggedShotEnemyCollisions.Clear();
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
                        if (reachedHitLimit(shot, enemy)) {
                            continue;
                        }
						if (isOverlapping(shotPos, enemy.Position, shotRadius, enemy.Radius)) {
							enemy.takeDamage(shot.Damage);
							shot.entityCollision(enemy.Position);
                            Logger.debug("Collision between {0} and {1}", shot, enemy);
                            if (shot.CollisionEnabled == false) {
                                //new PlaySoundCommand(AssetManager.getEnemyHitSound()).execute();
                                break;
                            } else {
                                addHitDetectionBetween(shot, enemy);
                            }
						}
					}
				}
			}            
		}

        private int generateKeyFrom(ShotBase shot, IEnemy enemy) {
            var value = shot.Identifier << 16 | enemy.Identifier;
            //Revert keys
            //var enemyId = value & UInt16.MaxValue;
            //var shotId = value >> 16;
            //--
            return value;
        }



        private bool reachedHitLimit(ShotBase shot, IEnemy enemy) {
            var value = generateKeyFrom(shot, enemy);
            if (_flaggedShotEnemyCollisions.ContainsKey(value) && _flaggedShotEnemyCollisions[value] >= 5) {
                return true;
            }
            return false;
        }

        private void addHitDetectionBetween(ShotBase shot, IEnemy enemy) {
            var value = generateKeyFrom(shot, enemy);
            if (!_flaggedShotEnemyCollisions.ContainsKey(value)) {
                _flaggedShotEnemyCollisions.Add(value, 1);
            } else {
                _flaggedShotEnemyCollisions[value] += 1;
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
                    new PlaySoundCommand(AssetManager.getPlayerHitSound()).execute();
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

		private void checkIfEngagingEnemiesCanBeginAttack() {
			List<IEnemy> activeEnemies = _enemyContainer.getEngagingEnemies();

			foreach (var enemy in activeEnemies) {
				Vector2 enemyPos = enemy.Position;
				float enemyAttackRadius = enemy.AttackRadius;
				if (isOverlapping(enemyPos, _player.Position, enemyAttackRadius, Player.Radius)) {
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

        internal void saveData() {
            Scoring.setKillsOnLevel(_killCount);
        }

        internal void showPopup(string s) {
            _gui.showOkPopup(s, null);
        }
    }
}

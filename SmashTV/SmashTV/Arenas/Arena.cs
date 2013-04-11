using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Enemies;
using MesserSmash.GUI;
using Helper = MesserSmash.SmashTVSystem;
using MesserSmash.Commands;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    public class Arena {
        public delegate void ArenaDelegate(Arena arena);
        public event ArenaDelegate onGameFinished;
        public event ArenaDelegate onZeroTimer;

        private Rectangle _bounds;
        private List<Spawnpoint> _spawnPoints;
        private float _timeSinceLastCreation;
        private const float ARENA_ENEMY_CREATION_CD = 0.65f;
        private List<LootType> _lootTable;
        private List<Loot> _activeLoot;
        protected float _secondsLeft;

        public enum LootType {
            Empty,
            Money,
            Health,
        }

        public Rectangle Bounds {
            get { return _bounds; }
            set { _bounds = value; }
        }

        public float TimeSinceLastCreation {
            get { return _timeSinceLastCreation; }
        }

        private Texture2D _texture;

        private float _timeInState;
        private States _state;
        private enum States {
            Starting,
            Running,
            End,
            Stopped,
        }

        public Arena() {
            _texture = AssetManager.getArenaTexture();
            _bounds = new Rectangle(40, 40, 850, 600);
            _timeSinceLastCreation = 0;
            _activeLoot = new List<Loot>();
            _secondsLeft = 120;
            _state = States.Running;
            _timeInState = 0;

            _spawnPoints = createSpawnpoints();
            _lootTable = createLootTable();
        }

        public void begin() {
            //EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Arena");
            Controller.instance.registerInterest(SpawnWaveCommand.NAME, onSpawnWave);
            Controller.instance.registerInterest(ReloadDatabaseCommand.NAME, onReloadDatabase);
            startLevel();
        }

        private void onReloadDatabase(ICommand cmd) {
            custReloadDatabase();
        }

        protected virtual void custReloadDatabase() {

        }

        public virtual void startLevel() {
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Arena");
        }

        private void onSpawnWave(ICommand cmd) {
            if (cmd is SpawnWaveCommand == false) {
                throw new Exception("Invalid command!");
            }
            custSpawnWaveCommand((cmd as SpawnWaveCommand).WaveSpawner);
            DataDefines.ID_STATE_ENEMIES_ALIVE = SmashTVSystem.Instance.EnemyContainer.getAliveEnemies().Count;
        }

        protected virtual void custSpawnWaveCommand(WaveSpawner cmd) {
            throw new Exception("Baseclass responsibility!");
        }

        public virtual Vector2 getPlayerStartPosition() {
            return new Vector2(_bounds.Center.X, _bounds.Center.Y);
        }

        protected virtual List<Spawnpoint> createSpawnpoints() {
            var spawns = new List<Spawnpoint>();
            spawns.Add(new Spawnpoint(new Rectangle(40, 40, 80, 80), _texture));
            spawns.Add(new Spawnpoint(new Rectangle(_bounds.Right - 80, 40, 80, 80), _texture));
            spawns.Add(new Spawnpoint(new Rectangle(40, _bounds.Bottom - 80, 80, 80), _texture));
            spawns.Add(new Spawnpoint(new Rectangle(_bounds.Right - 80, _bounds.Bottom - 80, 80, 80), _texture));
            return spawns;
        }

        protected virtual List<LootType> createLootTable() {
            return Utils.generateLootTable(927, 12, 3);
        }

        public void update(GameState state) {
            var gametime = state.DeltaTime;
            _timeSinceLastCreation = TimeSinceLastCreation + gametime;
            _timeInState += gametime;
            _secondsLeft -= gametime;
            onUpdate(state);
        }

        public void checkDebugInput() {
            if (_state == States.Running) {
                //debug                
                if (Utils.isNewKeyPress(Keys.G)) {
                    createEnemies(10);
                }
                if (Utils.isKeyDown(Keys.P)) {
                    createEnemies(5);
                }
                if (Utils.isNewKeyPress(Keys.R)) {
                    createRangedEnemies(10);
                }
                if (Utils.isNewKeyPress(Keys.H)) {
                    dropLoot(LootType.Health, Utils.randomPositionWithinRectangle(_bounds));
                }
                if (Utils.isNewKeyPress(Keys.M)) {
                    dropLoot(LootType.Money, Utils.randomPositionWithinRectangle(_bounds));
                }
                //--
            }
        }

        private void onUpdate(GameState state) {
            var gametime = state.DeltaTime;
            checkDebugInput();
            if (_state == States.Running) {
                foreach (var spawnPoint in _spawnPoints) {
                    spawnPoint.update(gametime);
                }
                foreach (var loot in getActiveLoot()) {
                    loot.update(gametime);
                }
                _showTimeLeft();
                custUpdate(state);
                if (_secondsLeft <= 0) {
                    _state = States.End;
                    EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.LevelFinished, this, "levelFinished");
                    _timeInState = 0;
                    if (onZeroTimer != null) {
                        onZeroTimer.Invoke(this);
                    }
                }
            } else if (_state == States.End) {
                if (_timeInState < 3.0f) {
                    foreach (var loot in getActiveLoot()) {
                        loot.update(gametime);
                    }
                } else {
                    _state = States.Stopped;
                    if (onGameFinished != null) {
                        onGameFinished.Invoke(this);
                    }
                }
            }
        }

        private void _showTimeLeft() {
            InfoWindow._timeLeft = Utils.secondsToMMSS((int)_secondsLeft);
        }

        public Spawnpoint getRandomSpawnpoint() {
            return _spawnPoints[Utils.randomInt(_spawnPoints.Count)];
        }

        protected virtual void custUpdate(GameState state) {
            if (canCreateEnemies()) {
                createEnemies(1);
            }
        }

        protected void createEnemies(int amount) {
            while (amount-- > 0) {
                getRandomSpawnpoint().generateRandomEnemies(1);
            }
            _timeSinceLastCreation = 0;
        }

        protected void createRangedEnemies(int amount) {
            while (amount-- > 0) {
                getRandomSpawnpoint().generateRangedEnemies(1);
            }
            _timeSinceLastCreation = 0;
        }
        protected void createSecondaryMeleeUnits(int amount) {
            while (amount-- > 0) {
                Spawnpoint sp = _spawnPoints[Utils.randomInt(_spawnPoints.Count)];
                sp.generateSecondaryMeleeUnits(1);
            }
            _timeSinceLastCreation = 0;
        }

        protected void createRangedSecondaryEnemies(int amount) {
            while (amount-- > 0) {
                Spawnpoint sp = _spawnPoints[Utils.randomInt(_spawnPoints.Count)];
                sp.generateSecondaryRangedEnemies(1);
            }
            _timeSinceLastCreation = 0;
        }

        protected bool canCreateEnemies() {
            if (TimeSinceLastCreation < ARENA_ENEMY_CREATION_CD)
                return false;

            return true;
        }

        public void drawBackground(SpriteBatch sb) {
            if (_state == States.Stopped)
                return;
            sb.Draw(_texture, _bounds, Color.Gray);
            if (_state != States.End) {
                foreach (var spawnpoint in _spawnPoints) {
                    spawnpoint.draw(sb);
                }
            }
        }

        public void drawLoot(SpriteBatch sb) {
            foreach (var loot in getActiveLoot()) {
                loot.draw(sb);
            }
        }

        public void handleDeadEnemy(Vector2 position, int killCount) {
            if (killCount < _lootTable.Count) {
                var loot = _lootTable[killCount];
                if (loot != LootType.Empty) {
                    dropLoot(loot, position);
                }
            }
        }

        private void dropLoot(LootType lootType, Vector2 pos) {
            _activeLoot.Add(new Loot(lootType, AssetManager.getHealthPackTexture(), AssetManager.getMoneyBagTexture(), pos));
        }

        public List<Loot> getActiveLoot() {
            return _activeLoot.FindAll(a => a.IsActive);
        }


        public float SecondsToFinish { get {return _secondsLeft; } }
        public float TimeSinceStart { get { return _timeInState; } }

        internal void abort() {
            Controller.instance.removeInterest(SpawnWaveCommand.NAME, onSpawnWave);
            Controller.instance.removeInterest(ReloadDatabaseCommand.NAME, onReloadDatabase);
            _spawnPoints.Clear();
        }
    }
}

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
using SharedSmashResources.Patterns;
using SharedSmashResources;

namespace MesserSmash.Arenas {
    public class Arena : IObserver {
        public delegate void ArenaDelegate(Arena arena);
        public event ArenaDelegate onArenaEnded;
        public event ArenaDelegate onArenaEnding;

        private Rectangle _bounds;
        private List<Spawnpoint> _spawnPoints;

        public List<Spawnpoint> SpawnPoints
        {
            get { return _spawnPoints; }
            private set { _spawnPoints = value; }
        }
        protected List<WaveSpawner> _spawners;
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

        public int Level { get; protected set; }


        public float TimeSinceLastCreation {
            get { return _timeSinceLastCreation; }
        }

        private Texture2D _texture;

        private float _timeInState;
        private States _state;

        public States State
        {
            get { return _state; }
            set { _state = value; }
        }
        public enum States {
            Starting,
            Running,
            Ending,
            Ended,
        }

        public Arena() {
            _texture = AssetManager.getArenaTexture();
            var scale = Utils.getResolutionScale();
            _bounds = new Rectangle((int)(322 * scale), (int)(90 * scale), (int)(1275 * scale), (int)(900 * scale));
            _timeSinceLastCreation = 0;
            _spawners = new List<WaveSpawner>();
            _activeLoot = new List<Loot>();
            _secondsLeft = 120;
            _state = States.Starting;
            _timeInState = 0;

            _spawnPoints = createSpawnpoints();
            _lootTable = createLootTable();
        }

        public void begin() {
            Controller.instance.addObserver(this);
            _state = States.Running;
            _timeInState = 0;
            new LevelStartedCommand(this).execute();
            custStartLevel();
        }

        private void onReloadDatabase(ICommand cmd) {
            custReloadDatabase();
        }

        protected virtual void custReloadDatabase() {

        }

        public virtual void custStartLevel() { }

        private void onSpawnWave(ICommand cmd) {
            if (cmd is SpawnWaveCommand == false) {
                throw new Exception("Invalid command!");
            }
            custSpawnWaveCommand((cmd as SpawnWaveCommand).WaveSpawner);
            DataDefines.ID_STATE_ENEMIES_ALIVE = SmashTVSystem.Instance.EnemyContainer.getAliveEnemies().Count;
        }

        protected virtual void custSpawnWaveCommand(WaveSpawner spawner) {
            spawner.deactivate();

            for (int i = 0; i < spawner.SpawnCount; i++) {
                var point = getRandomSpawnpoint();
                switch ((EnemyTypes.Types)spawner.EnemyType)
                {
                    case EnemyTypes.Types.Melee:
                        point.generateMeleeEnemies(1);
                        break;
                    case EnemyTypes.Types.SecondaryMelee:
                        point.generateSecondaryMeleeUnits(1);
                        break;
                    case EnemyTypes.Types.Range:
                        point.generateSecondaryRangedEnemies(1);
                        break;
                    case EnemyTypes.Types.RandomItem:
                        point.generateRandomEnemies(1);
                        break;
                    default:
                        throw new Exception("Unhandled enemy!");
                }
            }
        }

        public virtual Vector2 getPlayerStartPosition() {
            return new Vector2(_bounds.Center.X, _bounds.Center.Y);
        }

        protected virtual List<Spawnpoint> createSpawnpoints() {
            return Utils.generateSpawnpoints(Bounds);
        }

        protected virtual List<LootType> createLootTable() {
            Logger.info("-->CreateLootTable");
            var table = Utils.generateLootTable(200, 12, 0);
            for(int i =0; i < 200; ++i) {
                table.Add(Arena.LootType.Empty);
            }
            table[230] = LootType.Money;
            table[250] = LootType.Money;
            table[300] = LootType.Money;
            table[325] = LootType.Money;
            table[330] = LootType.Money;
            table[340] = LootType.Money;
            table[350] = LootType.Money;
            table[351] = LootType.Money;
            table[355] = LootType.Money;
            table[356] = LootType.Money;
            table[365] = LootType.Money;
            table[385] = LootType.Money;
            table[398] = LootType.Money;
            table[399] = LootType.Money;
            Logger.info("<--CreateLootTable randomStatus:{0}", MesserRandom.getStatus());
            return table;
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
                    createRangedSecondaryEnemies(10);
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
                foreach (var wave in _spawners)
                {
                    wave.update(state);
                }
                _showTimeLeft();
                custUpdate(state);
                if (_secondsLeft <= 0) {
                    handleArenaEnding();
                }
            } else if (_state == States.Ending) {
                if (_timeInState < 3.0f) {
                    foreach (var loot in getActiveLoot()) {
                        loot.update(gametime);
                    }
                } else {
                    handleArenaEnded();
                }
            }
        }

        protected void handleArenaEnding() {
            custArenaEnding();
            //_secondsLeft = 0;
            _state = States.Ending;
            _timeInState = 0;
            if (onArenaEnding != null) {
                onArenaEnding.Invoke(this);
            }
        }

        protected virtual void custArenaEnding() { }

        protected void handleArenaEnded() {
            _state = States.Ended;
            if (onArenaEnded != null) {
                onArenaEnded.Invoke(this);
            }
        }

        private void _showTimeLeft() {
            InfoWindow._timeLeft = Utils.secondsToMMSS((int)_secondsLeft);
        }

        public Spawnpoint getRandomSpawnpoint() {
            return _spawnPoints[Utils.randomInt(_spawnPoints.Count)];
        }

        protected virtual void custUpdate(GameState state) { }

        protected void createEnemies(int amount) {
            while (amount-- > 0) {
                getRandomSpawnpoint().generateRandomEnemies(1);
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
            if (_state == States.Ended)
                return;
            sb.Draw(_texture, _bounds, Color.Gray);
            if (_state != States.Ending) {
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
            new PlaySoundCommand(AssetManager.getLootDroppedSound()).execute();
            _activeLoot.Add(new Loot(lootType, AssetManager.getHealthPackTexture(), AssetManager.getMoneyBagTexture(), pos));
        }

        public List<Loot> getActiveLoot() {
            return _activeLoot.FindAll(a => a.IsActive);
        }


        public float SecondsToFinish { get {return _secondsLeft; } }
        //public float TimeSinceStart { get { return _timeInState; } }

        ~Arena() {
            clean();
        }

        internal void clean() {
            Controller.instance.removeObserver(this);
            if (_spawnPoints != null) {
                _spawnPoints.Clear();
                _spawnPoints = null;
            }
        }

        public void handleCommand(ICommand cmd) {
            //Controller.instance.addObserver(SpawnWaveCommand.NAME, onSpawnWave);
            //Controller.instance.addObserver(ReloadDatabaseCommand.NAME, onReloadDatabase);
            switch (cmd.Name) {
                case SpawnWaveCommand.NAME:
                    onSpawnWave(cmd);
                    break;
                case ReloadDatabaseCommand.NAME:
                    onReloadDatabase(cmd);
                    break;
            }
        }
    }
}

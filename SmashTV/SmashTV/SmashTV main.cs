using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Arenas;
using Microsoft.Xna.Framework.Input;
using MesserSmash.GUI;
using MesserSmash.Enemies;
using MesserSmash.Modules;
using System.IO;
using MesserSmash.Commands;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SharedSmashResources;

namespace MesserSmash {
    class SmashTV_main {
        private Microsoft.Xna.Framework.Content.ContentManager _content;
        private Microsoft.Xna.Framework.GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private Microsoft.Xna.Framework.Game _game;
        private Texture2D _circleTexture;
        private Texture2D _defaultTexture;
        private SpriteFont _defaultFont;

        private SmashTVSystem _smashTvSystem;
        private int _currentLevel;
        private float _betweenLevelTimer;
        private bool _waitingForTimer;
        private DebugGuiOverlay _debugGui;
        private bool _paused;
        private SoundManager _sound;
        private int _timeMultiplierIndex = 3;
        private List<float> _timeMultipliers = new List<float> { 0.25f, 0.5f, 0.75f, 1f, 1.25f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 6f, 8f };
        private StatusUpdate _states;
        private bool _playing;
        private float _deltaTime;
        private bool _replay;
        private StatusUpdate _loadedGame;
        private int _currentReplayIndex;

        public SmashTV_main(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Game game) {
            _content = Content;
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            _game = game;
            init();
        }

        private void init() {
            Logger.info("-------------------------------------------------------------------------");
            Logger.info("-------------------------------------------------------------------------");
            Logger.info("Game started at {0}", DateTime.Now.Ticks);
            Utils.tick();
            //Utils.initialize((int)DateTime.Now.Ticks);

            Controller.instance.registerInterest(RestartGameCommand.NAME, onRestartGame);
            new ReloadDatabaseCommand().execute();

            _graphics.PreferredBackBufferWidth = 1440;
            _graphics.PreferredBackBufferHeight = 800;
            Utils.setGameSize(1440, 800);
            _graphics.ApplyChanges();
            _game.IsMouseVisible = true;
            _circleTexture = _content.Load<Texture2D>("circle 64x64");
            _defaultTexture = _content.Load<Texture2D>("default");
            _defaultFont = _content.Load<SpriteFont>("Arial 12");
            AssetManager._player = _content.Load<Texture2D>("player64x64");
            AssetManager._shot = _circleTexture;
            AssetManager._arena = _defaultTexture;
            AssetManager._enemy = _circleTexture;
            AssetManager._rangedEnemy = _circleTexture;
            AssetManager._healthPack = _content.Load<Texture2D>("health_loot");
            AssetManager._moneyBag = _content.Load<Texture2D>("money_loot");
            AssetManager._rocketShot = _circleTexture;
            AssetManager._default = _defaultTexture;
            AssetManager._playerPortrait = _content.Load<Texture2D>("portrait");
            AssetManager._defaultFont = _defaultFont;
            AssetManager._bigGuiFont = _content.Load<SpriteFont>("BigGuiFont32");
            AssetManager._bgSound = _content.Load<SoundEffect>("background_music");
            AssetManager._failSound = _content.Load<SoundEffect>("fail");
            AssetManager._weaponSound = _content.Load<SoundEffect>("weapon");
            AssetManager._weaponReadySound = _content.Load<SoundEffect>("weapon_ready");
            _sound = new SoundManager();
            _sound.init();
            _states = new StatusUpdate();

            Scoring.reset();


            InfoWindow._defaultFont = _defaultFont;
            _smashTvSystem = new SmashTVSystem();
            _currentLevel = 0;
            _waitingForTimer = true;
            _betweenLevelTimer = 0;

            _debugGui = new DebugGuiOverlay(new Rectangle(40, 40, 850, 600));
        }

        private void onRestartGame(ICommand command) {
            Logger.info("onRestartGame");
            //Controller.instance.removeInterest(RestartGameCommand.NAME, onRestartGame);
            var cmd = command as RestartGameCommand;
            Scoring.reset();
            beginLevel(cmd.Level);
        }

        private void beginLevel(int level) {
            Logger.info("beginLevel");
            cleanOldData();
            Arena arena;
            var seed = (int)DateTime.Now.Ticks;
            Utils.initialize(seed);
            switch(level) {
                case 1:
                    arena = new Level1();
                    break;
                case 2:
                    arena = new Level2();
                    break;
                case 3:
                    arena = new Level3();
                    break;
                case 4:
                    arena = new Level4();
                    break;
                case 5:
                    arena = new Level5();
                    break;
                case -10:
                    _replay = true;
                    _currentReplayIndex = 0;
                    var gameid = "showcase2/level2_1.txt";
                    _loadedGame = new GameLoader(gameid, true).Replay;
                    Logger.info("Begin replay: {0}", gameid);
                    Utils.initialize(_loadedGame.Seed);
                    arena = new Level2();
                    break;
                default:
                    arena = new SpecialLevel();
                    break;
            }

            _currentLevel = level;
            _playing = true;
            Scoring.setLevel(_currentLevel);
            var player = new Player(arena.getPlayerStartPosition());
            arena.onGameFinished += new Arena.ArenaDelegate(onGameFinished);
            _smashTvSystem.startGame(arena, player, new ShotContainer(), new EnemyContainer());

            Controller.instance.registerInterest(PlayerDiedCommand.NAME, onPlayerDead);
        }

        void onPlayerDead(ICommand command) {
            var cmd = command as PlayerDiedCommand;
            _playing = false;
            saveState(_deltaTime);
            saveGame();
            Logger.info("Player died frames:{1}, random status: {0}", MesserRandom.getStatus(), _states.StoredStatesCount);
        }

        void onGameFinished(Arena arena) {
            _playing = false;
            _betweenLevelTimer = 0;
            _waitingForTimer = true;
            saveState(_deltaTime);
            saveGame();
            Logger.info("Game finished frames:{1} random status: {0}", MesserRandom.getStatus(), _states.StoredStatesCount);
        }

        private void cleanOldData() {
            _states.reset();
            _paused = false;
            Arena arena = SmashTVSystem.Instance.Arena;
            if (arena != null) {
                arena.onGameFinished -= onGameFinished;
                arena.abort();
            }
            _waitingForTimer = false;
            new ReloadDatabaseCommand().execute();
        }

        public void update(GameTime time) {
            handleGlobalInput();
            Logger.flush();

            //handle pause
            if (_paused && SmashTVSystem.Instance.Arena != null) {
                SmashTVSystem.Instance.Arena.checkDebugInput();
                return;
            }

            if (_replay) {
                if (_currentReplayIndex < _loadedGame.StoredStatesCount) {
                    Utils.forceState(_loadedGame, _currentReplayIndex);
                    _deltaTime = _loadedGame.DeltaTimes[_currentReplayIndex];
                } else {
                    Logger.info("Last frame: status={0}", MesserRandom.getStatus());
                    _paused = true;
                    _replay = false;
                    _playing = false;
                    return;
                }
            } else {
                _deltaTime = (float)time.ElapsedGameTime.TotalSeconds * _timeMultipliers[_timeMultiplierIndex];
            }


            _betweenLevelTimer += _deltaTime;
            _smashTvSystem.update(_deltaTime);
            if (_playing && !_replay) {
                saveState(_deltaTime);
            }
            _currentReplayIndex++;
            if (_waitingForTimer && _betweenLevelTimer >= 5) {
                _waitingForTimer = false;
                beginLevel(++_currentLevel);
                return;
            }
        }

        private void saveGame() {
            if (_replay) { return; }
            _states.Seed = Utils.Seed;
            new SaveGameCommand(_states);
        }

        private void saveState(float deltaTime) {
            _states.DeltaTimes.Add(deltaTime);
            _states.addKeyboard(Utils.getKeyboardState());            
            _states.addMouse(Utils.getMouseState());
        }

        private void handleGlobalInput() {
            ICommand cmd = null;
            if (Utils.getPressedKeys().Length > 0) {
                if (Utils.isNewKeyPress(Keys.Delete)) {
                    Logger.flush();
                    GC.Collect();
                }
                if (Utils.isNewKeyPress(Keys.OemPipe)) {
                    cmd = new RestartGameCommand(-10);
                } else if (Utils.isNewKeyPress(Keys.F1)) {
                    cmd = new RestartGameCommand(1);
                } else if (Utils.isNewKeyPress(Keys.F2)) {
                    cmd = new RestartGameCommand(2);
                } else if (Utils.isNewKeyPress(Keys.F3)) {
                    cmd = new RestartGameCommand(3);
                } else if (Utils.isNewKeyPress(Keys.F4)) {
                    cmd = new RestartGameCommand(4);
                } else if (Utils.isNewKeyPress(Keys.F5)) {
                    cmd = new RestartGameCommand(5);
                } else if (Utils.isNewKeyPress(Keys.F10)) {
                    cmd = new RestartGameCommand(10);
                }
                if (Utils.isNewKeyPress(Keys.Tab)) {
                    cmd = new ReloadDatabaseCommand();
                    _paused = false;
                }
                if (Utils.isNewKeyPress(Keys.Pause)) {
                    _paused = !_paused;
                }

                if (Utils.isEitherNewlyPressed(Keys.OemPlus, Keys.Add)) {
                    _timeMultiplierIndex = (int)MathHelper.Clamp(_timeMultiplierIndex + 1, 0, _timeMultipliers.Count - 1);
                } else if (Utils.isEitherNewlyPressed(Keys.OemMinus, Keys.Subtract)) {
                    _timeMultiplierIndex = (int)MathHelper.Clamp(_timeMultiplierIndex - 1, 0, _timeMultipliers.Count - 1);
                }
            }

            Utils.tick();
            if (cmd != null) {
                cmd.execute();
            }
        }

        public void draw(GameTime time) {
            _smashTvSystem.draw(_spriteBatch);
            InfoWindow.draw(_spriteBatch);
            _debugGui.draw(_spriteBatch);
        }
    }
}


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
        private int _currentLevelIndex;
        private float _timeInState;
        private bool _waitingForTimer;
        private DebugGuiOverlay _debugGui;
        private bool _paused;
        private SoundManager _sound;

        public SmashTV_main(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Game game) {
            _content = Content;
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            _game = game;
            init();
        }

        private void init() {
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
            AssetManager._healthPack = _circleTexture;
            AssetManager._moneyBag = _circleTexture;
            AssetManager._rocketShot = _circleTexture;
            AssetManager._default = _defaultTexture;
            AssetManager._playerPortrait = _content.Load<Texture2D>("portrait");
            AssetManager._defaultFont = _defaultFont;
            AssetManager._bigGuiFont = _content.Load<SpriteFont>("BigGuiFont32");
            AssetManager._bgSound = _content.Load<SoundEffect>("background_music");
            AssetManager._failSound = _content.Load<SoundEffect>("fail");
            AssetManager._weaponSound = _content.Load<SoundEffect>("weapon");
            _sound = new SoundManager();
            _sound.init();


            InfoWindow._defaultFont = _defaultFont;
            _smashTvSystem = new SmashTVSystem();
            _currentLevelIndex = 1;
            _waitingForTimer = true;
            _timeInState = 0;

            _debugGui = new DebugGuiOverlay(new Rectangle(40, 40, 850, 600));
        }

        private void launchArena(int level) {
            new ReloadDatabaseCommand().execute();
            _paused = false;            
            Arena arena = SmashTVSystem.Instance.Arena;
            if (arena != null) {
                arena.onGameFinished -= onGameFinished;
            }
            _waitingForTimer = false;
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
                default:
                    arena = new SpecialLevel();
                    break;
            }
            var player = new Player(arena.getPlayerStartPosition());
            arena.onGameFinished += new Arena.ArenaDelegate(onGameFinished);
            _smashTvSystem.startGame(arena, player, new ShotContainer(), new EnemyContainer());
        }

        void onGameFinished(Arena arena) {
            _timeInState = 0;
            _waitingForTimer = true;
        }

        public void update(GameTime time) {
            
            handleGlobalInput();
            //handle pause
            if (_paused) {
                SmashTVSystem.Instance.Arena.checkDebugInput();
                return;
            }

            float deltaTime = (float)time.ElapsedGameTime.TotalSeconds;
            _timeInState += deltaTime;
            if (_waitingForTimer && _timeInState >= 5) {
                _waitingForTimer = false;
                launchArena(++_currentLevelIndex);
            }
            _smashTvSystem.update(deltaTime);
        }

        private void handleGlobalInput() {
            if (Utils.isNewKeyPress(Keys.Delete)) {
                Logger.flush();
                GC.Collect();
            }
            if (Utils.isNewKeyPress(Keys.F1)) {
                launchArena(1);
            } else if (Utils.isNewKeyPress(Keys.F2)) {
                launchArena(2);
            } else if (Utils.isNewKeyPress(Keys.F3)) {
                launchArena(3);
            } else if (Utils.isNewKeyPress(Keys.F4)) {
                launchArena(4);
            } else if (Utils.isNewKeyPress(Keys.F5)) {
                launchArena(5);
            } else if (Utils.isNewKeyPress(Keys.F10)) {
                launchArena(10);
            }
            if (Utils.isNewKeyPress(Keys.Tab)) {
                new ReloadDatabaseCommand().execute();
                _paused = false;
            }
            if (Utils.isNewKeyPress(Keys.Pause)) {
                _paused = !_paused;
            }

            Utils.tickInputStates();
        }

        public void draw(GameTime time) {
            _smashTvSystem.draw(_spriteBatch);
            InfoWindow.draw(_spriteBatch);
            _debugGui.draw(_spriteBatch);
        }
    }
}


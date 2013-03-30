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

        public SmashTV_main(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Game game) {
            _content = Content;
            _graphics = graphics;
            _spriteBatch = spriteBatch;
            _game = game;
            init();
        }

        private void init() {
            reloadDatabase();

            _graphics.PreferredBackBufferWidth = 1440;
            _graphics.PreferredBackBufferHeight = 800;
            Utils.setGameSize(1440, 800);
            _graphics.ApplyChanges();
            _game.IsMouseVisible = true;
            _circleTexture = _content.Load<Texture2D>("circle 64x64");
            _defaultTexture = _content.Load<Texture2D>("default");
            _defaultFont = _content.Load<SpriteFont>("Arial 12");
            TextureManager._player = _content.Load<Texture2D>("player64x64");
            TextureManager._shot = _circleTexture;
            TextureManager._arena = _defaultTexture;
            TextureManager._enemy = _circleTexture;
            TextureManager._rangedEnemy = _circleTexture;
            TextureManager._healthPack = _circleTexture;
            TextureManager._moneyBag = _circleTexture;
            TextureManager._rocketShot = _circleTexture;
            TextureManager._default = _defaultTexture;
            TextureManager._playerPortrait = _content.Load<Texture2D>("portrait");
            TextureManager._defaultFont = _defaultFont;
            TextureManager._bigGuiFont = _content.Load<SpriteFont>("BigGuiFont32");

            InfoWindow._defaultFont = _defaultFont;
            _smashTvSystem = new SmashTVSystem();
            _currentLevelIndex = 1;
            _waitingForTimer = true;
            _timeInState = 0;

            _debugGui = new DebugGuiOverlay(new Rectangle(40, 40, 850, 600));
        }

        private void reloadDatabase() {
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory);
            using (StreamReader sr = new StreamReader("./database.txt")) {
                SmashDb.populateJson(sr);
            }
        }

        private void launchArena(int level) {
            reloadDatabase();
            _paused = false;
            Arena arena = null;
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
            _currentLevelIndex++;
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
                launchArena(_currentLevelIndex);
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
                reloadDatabase();
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


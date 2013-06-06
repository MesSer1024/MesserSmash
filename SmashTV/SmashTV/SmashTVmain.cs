﻿using System;
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
using SharedSmashResources.Patterns;

namespace MesserSmash {
	class SmashTV_main : IObserver {
		private Microsoft.Xna.Framework.Content.ContentManager _content;
		private Microsoft.Xna.Framework.GraphicsDeviceManager _graphics;
		private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
		private Microsoft.Xna.Framework.Game _game;
        private Texture2D _circleTexture;
        private Texture2D _rombTexture;
		private Texture2D _defaultTexture;
		private SpriteFont _defaultFont;

		private SmashTVSystem _smashTvSystem;
		private int _currentLevel;
		private float _betweenLevelTimer;
		private bool _waitingForTimer;
		private bool _paused;
		private SoundManager _sound;
		private int _timeMultiplierIndex = 3;
		private List<float> _timeMultipliers = new List<float> { 0.25f, 0.5f, 0.75f, 1f, 1.25f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 6f, 8f };
		private GameStates _states;
		private bool _playing;
		private GameState _state;
		private bool _replay;
		private GameStates _loadedGame;
		private int _currentReplayIndex;
		private bool _hasUsername;
		private IScreen _screen;
		private float _timeSinceLevelStart;

		public SmashTV_main(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Game game) {
			_content = Content;
			_graphics = graphics;
			_spriteBatch = spriteBatch;
			_game = game;
			_hasUsername = false;
			init();
		}

		private void init() {
			Logger.init();
			Logger.info("-------------------------------------------------------------------------");
			Logger.info("-------------------------------------------------------------------------");
			Logger.info("Game started at {0}", DateTime.Now.Ticks);
			Utils.tick();
			//Utils.initialize((int)DateTime.Now.Ticks);

			//Controller.instance.addObserver(RestartGameCommand.NAME, onRestartGame);
			Controller.instance.addObserver(this);
			new ReloadDatabaseCommand().execute();

			_graphics.PreferredBackBufferWidth = 1440;
			_graphics.PreferredBackBufferHeight = 800;
			Utils.setGameSize(1440, 800);
			_graphics.ApplyChanges();
			_game.IsMouseVisible = true;
            _circleTexture = _content.Load<Texture2D>("circle 64x64");
            _rombTexture = _content.Load<Texture2D>("rectangle_64x64");
			_defaultTexture = _content.Load<Texture2D>("default");
			_defaultFont = _content.Load<SpriteFont>("Arial 12");
			AssetManager._player = _content.Load<Texture2D>("player64x64");
			AssetManager._shot = _circleTexture;
			AssetManager._arena = _defaultTexture;
			AssetManager._enemy = _circleTexture;
			AssetManager._rangedEnemy = _rombTexture;
			AssetManager._healthPack = _content.Load<Texture2D>("health_loot");
			AssetManager._moneyBag = _content.Load<Texture2D>("money_loot");
			AssetManager._rocketShot = _circleTexture;
			AssetManager._default = _defaultTexture;
            AssetManager._controllerLayout = _content.Load<Texture2D>("smashTV_controls");
			AssetManager._playerPortrait = _content.Load<Texture2D>("portrait");
			AssetManager._defaultFont = _defaultFont;
			AssetManager._bigGuiFont = _content.Load<SpriteFont>("BigGuiFont32");
			AssetManager._bgSound = _content.Load<SoundEffect>("background_music");
			AssetManager._failSound = _content.Load<SoundEffect>("fail");
			AssetManager._weaponSound = _content.Load<SoundEffect>("weapon");
			AssetManager._weaponReadySound = _content.Load<SoundEffect>("weapon_ready");
			_sound = new SoundManager();
			_sound.init();
			_states = new GameStates();

			Scoring.reset();


			InfoWindow._defaultFont = _defaultFont;
			_smashTvSystem = new SmashTVSystem();
			_currentLevel = 0;
			_waitingForTimer = true;
			_betweenLevelTimer = 0;

			new LoadConfigFileCommand().execute();
            //new RequestHighscoresCommand(1).execute();
		}

		private void onRestartGame(ICommand command) {
			Logger.info("onRestartGame");
			var cmd = command as RestartGameCommand;
			prepareNewLevel(cmd.Level, true);
		}

		private void prepareNewLevel(int level, bool resetScore = false) {
			Logger.info("prepareNewLevel");
			if (resetScore) {
				Scoring.reset();
			}
			cleanOldData();
			Arena arena;
			var seed = (int)DateTime.Now.Ticks;
			Utils.initialize(seed);
			if (level < 0) {
				_replay = true;
				_currentReplayIndex = 0;
                var gameid = SmashTVSystem.Instance.ReplayPath;
				_loadedGame = new GameLoader(gameid, true).Replay;
				Logger.info("prepareNewLevel replay: {0} -- version {1}", gameid, _loadedGame.GameVersion);
                //if (_loadedGame.GameVersion != SmashTVSystem.Instance.GameVersion) {
                //    throw new Exception(Utils.makeString("Invalid GameVersions! {0}", SmashTVSystem.Instance.GameVersion));
                //}
				Utils.initialize(_loadedGame.Seed);
				arena = buildLevel(_loadedGame.Level);
				_currentLevel = _loadedGame.Level;
			} else {
				arena = buildLevel(level);
				_currentLevel = level;
			}
			_states.Level = _currentLevel;
			Scoring.setLevel(_currentLevel);
			_playing = true;
			var player = new Player(arena.getPlayerStartPosition());
			arena.onGameFinished += new Arena.ArenaDelegate(onGameFinished);
			_smashTvSystem.initLevel(arena, player, new ShotContainer(), new EnemyContainer(), _replay);
		}

		private Arena buildLevel(int level) {
			Arena arena;
			switch (level) {
				case 1:
					arena = new Level01();
					break;
				case 2:
					arena = new Level02();
					break;
				case 3:
					arena = new Level03();
					break;
				case 4:
					arena = new Level04();
					break;
				case 5:
					arena = new Level05();
                    break;
                case 6:
                    arena = new Level06();
                    break;
                case 7:
                    arena = new Level07();
                    break;
                case 8:
                    arena = new Level08();
                    break;
                case 9:
                    arena = new Level09();
                    break;
                case 10:
                    arena = new Level10(); //BOSS LEVEL!
                    break;
                case 11:
                    arena = new Level11();
                    break;
                case 12:
                    arena = new Level12();
                    break;
                case 13:
                    arena = new Level13();
                    break;
                case 14:
                    arena = new Level14();
                    break;
                case 15:
                    arena = new Level15();
                    break;
                case 16:
                    arena = new Level16();
                    break;
				default:
					arena = new SpecialLevel();
					Logger.error("unknown level={0}", level);
					break;
			}
			return arena;
		}

		private void onConfigFileLoaded(ICommand cmd) {
			var command = cmd as LoadConfigFileCommand;
            SmashTVSystem.Instance.ServerIp = command.ServerIp;
            SmashTVSystem.Instance.ReplayPath = command.ReplayPath;
            SmashTVSystem.Instance.GameVersion = command.GameVersion;
            _hasUsername = command.HasUsername;
            if (_hasUsername) {
				SmashTVSystem.Instance.Username = command.Username;
			} else {
				_screen = new NewUserScreen();
			}
		}

		private void onRegisterUsername(ICommand cmd) {
			var command = cmd as RegisterUsernameCommand;
			_hasUsername = command.HasUsername;
			if (_hasUsername) {
				SmashTVSystem.Instance.Username = command.Username;
				_screen.destroy();
				_screen = null;
			}
		}

		void onPlayerDead(ICommand command) {
			var cmd = command as PlayerDiedCommand;
			_playing = false;
			saveInputState(_state);
			saveGame();
            if (!_replay) {
                new RegisterHighscoreCommand(_states.UserName);
                _smashTvSystem.showGameOverScreen();
            }
			Logger.info("Player died frames:{1}, random status: {0}", MesserRandom.getStatus(), _states.StoredStatesCount);
		}

		void onGameFinished(Arena arena) {
			_playing = false;
			_betweenLevelTimer = 0;
			_waitingForTimer = true;
			saveInputState(_state);
			saveGame();

			Logger.info("Game finished frames:{1} random status: {0}", MesserRandom.getStatus(), _states.StoredStatesCount);
		}

		private void onClientReady(ICommand cmd) {
			_timeSinceLevelStart = 0;
			_smashTvSystem.startLoadedLevel();
		}

		private void cleanOldData() {
			_states.reset();
			Logger.init();
			_paused = false;
			Arena arena = SmashTVSystem.Instance.Arena;
			if (arena != null) {
				arena.onGameFinished -= onGameFinished;
				arena.clean();
			}
			_waitingForTimer = false;
			new ReloadDatabaseCommand().execute();
		}

		public void update(GameTime time) {
			//populate input and deltatime
			float deltaTime;
			if (_replay) {
				if (_currentReplayIndex < _loadedGame.StoredStatesCount) {
					Utils.forceState(_loadedGame, _currentReplayIndex);
					deltaTime = _loadedGame.DeltaTimes[_currentReplayIndex];
					_currentReplayIndex++;
				} else {
					Logger.info("Last replay frame: status={0}", MesserRandom.getStatus());
					_paused = true;
					_replay = false;
					_playing = false;
					return;
				}
			} else {
                deltaTime = (float)time.ElapsedGameTime.TotalSeconds * _timeMultipliers[_timeMultiplierIndex];
                Utils.tick();
                if (!_hasUsername) {
                    _screen.update(deltaTime);
					return;
				}

				handleGlobalInput(); //only run global input on non-replay
				//handle pause
				if (_paused && SmashTVSystem.Instance.Arena != null) {
					SmashTVSystem.Instance.Arena.checkDebugInput();
					return;
				}
				_betweenLevelTimer += deltaTime;
				if (_waitingForTimer && _betweenLevelTimer >= 5) {
					_waitingForTimer = false;
					prepareNewLevel(++_currentLevel);
					return;
				}
			}
			_timeSinceLevelStart += deltaTime;
			_state = new GameState(deltaTime, _timeSinceLevelStart);
			_smashTvSystem.update(_state);
			if (_playing && !_replay) {
				saveInputState(_state);
			}
		}

		private void saveGame() {
			if (_replay) { return; }
            _smashTvSystem.saveData();
			_states.Seed = Utils.Seed;
            _states.UserName = SmashTVSystem.Instance.Username;
            _states.UserId = SmashTVSystem.Instance.UserId;
            _states.GameId = SmashTVSystem.Instance.GameId;
            _states.GameVersion = SmashTVSystem.Instance.GameVersion;

            var levelInfo = Scoring.getLevelScores()[Scoring.getLevelScores().Count - 1];
            _states.Kills = levelInfo.Kills;
            _states.Score = levelInfo.Score;

            double totalTime = 0;
            foreach (var i in _states.DeltaTimes) {
                totalTime += i;
            }
            _states.TotalGametime = totalTime;

			new SaveGameCommand(_states);
		}

		private void saveInputState(GameState state) {
			_states.DeltaTimes.Add(state.DeltaTime);
			_states.addKeyboard(Utils.getKeyboardState());            
			_states.addMouse(Utils.getMouseState());
		}

		private void handleGlobalInput() {
			ICommand cmd = null;
			if (Utils.getPressedKeys().Length > 0) {
				if (Utils.isNewKeyPress(Keys.Delete)) {
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
                } else if (Utils.isNewKeyPress(Keys.F6)) {
                    cmd = new RestartGameCommand(6);
                } else if (Utils.isNewKeyPress(Keys.F7)) {
                    cmd = new RestartGameCommand(7);
                } else if (Utils.isNewKeyPress(Keys.F8)) {
                    cmd = new RestartGameCommand(8);
                } else if (Utils.isNewKeyPress(Keys.F9)) {
                    cmd = new RestartGameCommand(9);
                } else if (Utils.isNewKeyPress(Keys.F10)) {
					cmd = new RestartGameCommand(10);
                } else if (Utils.isNewKeyPress(Keys.F11)) {
                    cmd = new RestartGameCommand(11);
                } else if (Utils.isNewKeyPress(Keys.F12)) {
                    cmd = new RestartGameCommand(12);
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

			if (cmd != null) {
				cmd.execute();
			}
		}

		public void draw(GameTime time) {
			if (!_hasUsername) {
				_screen.draw(_spriteBatch);
				return;
			}
			_smashTvSystem.draw(_spriteBatch);
			InfoWindow.draw(_spriteBatch);
		}

		internal void exit() {
			Logger.clean();
		}

		public void handleCommand(ICommand cmd) {
			switch (cmd.Name) {
				case RestartGameCommand.NAME:
					onRestartGame(cmd);
					break;
				case PlayerDiedCommand.NAME:
					onPlayerDead(cmd);
					break;
				case LoadConfigFileCommand.NAME:
					onConfigFileLoaded(cmd);
					break;
				case ClientReadyCommand.NAME:
					onClientReady(cmd);
					break;
                case RegisterUsernameCommand.NAME:
                    onRegisterUsername(cmd);
                    break;
			}
		}
	}
}


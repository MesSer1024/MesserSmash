using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Commands;
using MesserSmash.Modules;

namespace MesserSmash.GUI {
    class GUIMain {
        public static GUIMain Instance {get; set;}
        private Rectangle _background;
        private Color _backgroundColor;
        private Rectangle _playerHudBackground;
        private Rectangle _portraitPosition;
        private Healthbar _health;
        private Healthbar _energy;

        private ShortcutButton _btnLMB;
        private ShortcutButton _btnRMB;
        private ShortcutButton _boost;
        private FunnyText _killCounter;
        private FunnyText _secondsLeft;
        private bool _inGame;
        private float _score;
        private FunnyText _scoreField;
        private float _timeDead;
        private FunnyText _recharge;
        private float _timeRechargeShown;
        private bool _loadingScreenVisible;

        private DebugGuiOverlay _debugGui;


        public GUIMain() {
            Instance = this;
            _inGame = true;
            _loadingScreenVisible = false;
            _timeRechargeShown = 100;
            int h = 120;
            _background = new Rectangle(0, Utils.getGameHeight() - h, Utils.getGameWidth(), Utils.getGameHeight());
            _backgroundColor = Color.YellowGreen;
            _portraitPosition = new Rectangle(200, _background.Y + 20, 80, 80);
            _health = new Healthbar(new Rectangle(_portraitPosition.Right + 5, _portraitPosition.Center.Y - 23, 150, 20));
            _energy = new Healthbar(new Rectangle(_portraitPosition.Right + 5, _health.Bounds.Bottom + 6, 150, 20));
            _health._valueColor = Color.Yellow;
            _energy._valueColor = Color.LightBlue;
            _playerHudBackground = new Rectangle(_portraitPosition.X - 110, _portraitPosition.Y - 10, _health.Bounds.Right + 120 - _portraitPosition.Left, _portraitPosition.Height + 20);
            _boost = new ShortcutButton(new Rectangle(_playerHudBackground.Right + 20, _playerHudBackground.Bottom - 25, 40, 25));
            _btnLMB = new ShortcutButton(new Rectangle(_playerHudBackground.Right + 20, _playerHudBackground.Top + 5, 60, 30));
            _btnRMB = new ShortcutButton(new Rectangle(_btnLMB.Bounds.Right + 10, _playerHudBackground.Top + 5, 60, 30));
            _boost.setText("Run");
            _btnLMB.setText("Pistol");
            _btnRMB.setText("Rocket");
            _scoreField = new FunnyText("0", new Rectangle { X = _btnRMB.Bounds.Right, Y = _btnRMB.Bounds.Top, Height = 80, Width = _background.Right - _btnRMB.Bounds.Right });

            _killCounter = new FunnyText("0", new Rectangle(_playerHudBackground.Left, _portraitPosition.Top, _portraitPosition.Left - _playerHudBackground.Left, _portraitPosition.Height));
            var gameScreen = SmashTVSystem.Instance.Arena.Bounds;
            _secondsLeft = new FunnyText("100", new Rectangle(gameScreen.Left, gameScreen.Top, gameScreen.Width, gameScreen.Height));
            _secondsLeft.TextColor = Color.LightGoldenrodYellow;
            _secondsLeft.Visible = false;
            _secondsLeft.TextScale = 2.25f;

            _debugGui = new DebugGuiOverlay(new Rectangle(40, 40, 850, 600));
        }

        public void update(float gametime) {
            if (_loadingScreenVisible) {
                if (Utils.isNewKeyPress(Keys.Space)) {
                    performClientReady();
                }
            } else if (_inGame) {
                _boost.setMode(Utils.isKeyDown(Keys.LeftControl));
                _btnLMB.setMode(Utils.LmbPressed);
                _btnRMB.setMode(Utils.RmbPressed);
                if(_recharge != null) {
                    _timeRechargeShown += gametime;
                    if (_timeRechargeShown >= 0.75f) {
                        _recharge = null;
                    }
                }
            } else {
                _timeDead += gametime;
                if (_timeDead > 1.0f) {
                    if (Utils.isNewKeyPress(Keys.R)) {
                        new RestartGameCommand(1).execute();
                    }
                    else if (Utils.isNewKeyPress(Keys.Enter)) {
                        new RestartGameCommand(SmashTVSystem.Instance.Arena.Level).execute();
                    }
                }
            }
        }

        public void performClientReady() {
            new ClientReadyCommand().execute();
            _loadingScreenVisible = false;
        }
        private bool validHighscoreCharacter(Keys key) {
            return (key >= Keys.A && key <= Keys.Z) || (key >= Keys.D0 && key <= Keys.D9);
        }

        public void setPlayerHealth(int value) {
            _health.Value = value;
        }

        public void setPlayerEnergy(int value) {
            _energy.Value = value;
        }

        public void setKillCount(int value) {
            _killCounter.Text = value.ToString();
        }

        public void draw(SpriteBatch sb) {
            if (_loadingScreenVisible) {
                sb.Draw(AssetManager.getControlsTexture(), new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight()), Color.White);

                var text = new FunnyText(String.Format("Press <space> to start Level{0}", SmashTVSystem.Instance.Arena.Level), new Rectangle { X = 0, Y = Utils.getGameHeight() - 100, Width = Utils.getGameWidth(), Height = 0 });
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
                return;
            }

            _debugGui.draw(sb);

            if (_inGame) {
                var text = new FunnyText(String.Format("Level {0}", SmashTVSystem.Instance.Arena.Level), new Rectangle { X = SmashTVSystem.Instance.Arena.Bounds.X, Y = 0, Width = SmashTVSystem.Instance.Arena.Bounds.Width, Height = 50 });
                text.VerticalCenter = true;
                text.Draw(sb);
                sb.Draw(AssetManager.getDefaultTexture(), _background, _backgroundColor);
                sb.Draw(AssetManager.getDefaultTexture(), _playerHudBackground, Color.Black);
                sb.Draw(AssetManager.getPortraitTexture(), _portraitPosition, Color.White);
                _health.draw(sb);
                _energy.draw(sb);
                _boost.draw(sb);
                _btnLMB.draw(sb);
                _btnRMB.draw(sb);
                _killCounter.Draw(sb);
                _secondsLeft.Draw(sb);
                _scoreField.Draw(sb);
                if (_recharge != null) {
                    _recharge.Draw(sb);
                }
            } else {
                var r = new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight());
                sb.Draw(AssetManager.getDefaultTexture(), r, Color.Black);

                {
                    var text = new FunnyText("GAME OVER!", new Rectangle { X = 50, Y = 0, Width = Utils.getGameWidth(), Height = 75 });
                    text.HorizontalCenter = false;
                    text.Draw(sb);
                }
                {
                    var text = new FunnyText(Utils.makeString("Your Score: {0}", formatScorePoints(_score)), new Rectangle { X = 50, Y = 75, Width = Utils.getGameWidth(), Height = 75 });
                    text.HorizontalCenter = false;
                    text.Draw(sb);
                }
                {
                    var text = new FunnyText(Utils.makeString("Press <enter> to retry level"), new Rectangle(50, 200, Utils.getGameWidth(), 75));
                    text.HorizontalCenter = false;
                    text.Draw(sb);
                }
                {
                    var text = new FunnyText(Utils.makeString("Press <r> to restart game"), new Rectangle(50, 250, Utils.getGameWidth(), 75));
                    text.HorizontalCenter = false;
                    text.Draw(sb);
                }

                for (int i = 0; i < Math.Min(8, Highscore.Instance.Score.Count); i++) {
                    var score = Highscore.Instance.Score[i];
                    var name = Highscore.Instance.Players[i];
                    var kills = Highscore.Instance.Kills[i];
                    var foo = new FunnyText(name + " - " + score + " - " + kills, new Rectangle(100, 300 + 50 * i, Utils.getGameWidth(), 75));
                    foo.HorizontalCenter = false;
                    foo.Draw(sb);
                }
            }
        }

        private string formatScorePoints(float score) {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            var formattedNumber = string.Format(culture, "{0:n}", _score);
            return formattedNumber;
        }

        public void setSecondsLeft(float time) {
            if (Utils.valueBetween(time, 0, 9)) {
                //use countdown in middle of screen
                _secondsLeft.Text = ((int)time).ToString();
                _secondsLeft.Visible = true;
                //scale : [1..3]    timespan :  [10..1]
                var numeric = time/10;
                _secondsLeft.TextScale = (1 - numeric) * 2 + 1;
            } else {
                _secondsLeft.Visible = false;
            }
        }

        public void showGameOver() {
            Highscore.Instance.load();
            _inGame = false;
            _loadingScreenVisible = false;
            _timeDead = 0;
        }

        public void restart() {
            _inGame = true;
            _loadingScreenVisible = false;
        }

        public void setScore(float newScore) {
            _score = newScore;
            _scoreField.Text = Utils.makeString("Score: {0}", formatScorePoints(_score));
        }

        public void showWeaponRecharged() {
            _recharge = new FunnyText("Weapon Recharged!", new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight()));
            _recharge.TextColor = Color.NavajoWhite;
            _timeRechargeShown = 0;
        }

        internal void showLoadingScreen() {
            _loadingScreenVisible = true;
        }
    }
}

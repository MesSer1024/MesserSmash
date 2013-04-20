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
        private StringBuilder _gameoverName;
        private float _timeDead;
        private bool _saved;

        public GUIMain() {
            _gameoverName = new StringBuilder();
            _inGame = true;
            _saved = false;
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
        }

        public void update(float gametime) {
            if (_inGame) {
                _boost.setMode(Utils.isKeyDown(Keys.LeftControl));
                _btnLMB.setMode(Mouse.GetState().LeftButton == ButtonState.Pressed);
                _btnRMB.setMode(Mouse.GetState().RightButton == ButtonState.Pressed);
            } else {
                _timeDead += gametime;
                if (_timeDead > 1.24f) {
                    foreach (var key in Utils.getPressedKeys()) {
                        if (key == Keys.Back && _saved == false) {
                            if (_gameoverName.Length > 0) {
                                _gameoverName.Remove(_gameoverName.Length - 1, 1);
                            }
                        } else if (key == Keys.Enter && Utils.isNewKeyPress(key)) {
                            if (_saved == false) {
                                new RegisterHighscoreCommand(_gameoverName.ToString()).execute();
                                _saved = true;
                            } else {
                                new StartGameCommand(1).execute();
                            }
                        } else {
                            if (validHighscoreCharacter(key) && Utils.isNewKeyPress(key) && _saved == false) {
                                _gameoverName.Append(key.ToString());
                            }
                        }
                    }
                }
            }
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
            if (_inGame) {
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
            } else {
                var r = new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight());
                sb.Draw(AssetManager.getDefaultTexture(), r, Color.Black);

                var text = new FunnyText("Game Over", new Rectangle { X = 0, Y = 0, Width = Utils.getGameWidth(), Height = 75});
                text.HorizontalCenter = false;
                text.Draw(sb);
                if (_saved == false) {
                    var text3 = new FunnyText(Utils.makeString("Your Score: {0}", formatScorePoints(_score))
                        , new Rectangle { X = 0, Y = 75, Width = Utils.getGameWidth(), Height = 75 });
                    text3.HorizontalCenter = false;
                    var text2 = new FunnyText(Utils.makeString("Enter Name: {0}", _gameoverName.ToString())
                        , new Rectangle { X = 0, Y = 150, Width = Utils.getGameWidth(), Height = 75 });
                    text2.HorizontalCenter = false;
                    text3.Draw(sb);
                    text2.Draw(sb);
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
            _saved = false;
        }

        public void restart() {
            _inGame = true;
            _saved = false;
        }

        public void setScore(float newScore) {
            _score = newScore;
            _scoreField.Text = Utils.makeString("Score: {0}", formatScorePoints(_score));
        }
    }
}

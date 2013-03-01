using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MesserSmash.GUI {
    class GUIMain {
        private Rectangle _background;
        private Color _backgroundColor;
        private Rectangle _playerHudBackground;
        private Rectangle _portraitPosition;
        private Healthbar _health;
        private Healthbar _energy;

        private List<ShortcutButton> _debugButtons;

        private ShortcutButton _btnLMB;
        private ShortcutButton _btnRMB;
        private ShortcutButton _boost;
        private FunnyText _killCounter;
        private FunnyText _secondsLeft;

        public GUIMain() {
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

            _killCounter = new FunnyText("0", new Rectangle(_playerHudBackground.Left, _portraitPosition.Top, _portraitPosition.Left - _playerHudBackground.Left, _portraitPosition.Height));
            var gameScreen = SmashTVSystem.Instance.Arena.Bounds;
            _secondsLeft = new FunnyText("100", new Rectangle(gameScreen.Left, gameScreen.Top, gameScreen.Width, gameScreen.Height));
            _secondsLeft.TextColor = Color.LightGoldenrodYellow;
            _secondsLeft.Visible = false;
            _secondsLeft.TextScale = 2.25f;
            
            //debug buttons
            addDebugButton("[W-A-S-D] Movements");
            addDebugButton("[LMB-RMB] Fire weapons");
            addDebugButton("[Ctrl] speed booster!");
            addDebugButton("-----");
            addDebugButton("[G] Create 10 Random");
            addDebugButton("[P_holdable] Create 5 Random");
            addDebugButton("[R] Create 5 Ranged");
            addDebugButton("[H] Drop Health pack");
            addDebugButton("[M] Drop Money");
            addDebugButton("[Del] Force GC & Output");

            addDebugButton("[F1] Start Level1");
            addDebugButton("[F2] Start Level2");
            addDebugButton("[F3] Start Level3");
            addDebugButton("[F4] Start Level4");
            addDebugButton("[F5] Start Level5");
            addDebugButton("[F10] Start Special Level");
        }

        public void update(float gametime) {
            _boost.setMode(Utils.isKeyDown(Keys.LeftControl));
            _btnLMB.setMode(Mouse.GetState().LeftButton == ButtonState.Pressed);
            _btnRMB.setMode(Mouse.GetState().RightButton == ButtonState.Pressed);
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
            foreach (var button in _debugButtons) {
                button.draw(sb);
            }
            sb.Draw(TextureManager.getDefaultTexture(), _background, _backgroundColor);
            sb.Draw(TextureManager.getDefaultTexture(), _playerHudBackground, Color.Black);
            sb.Draw(TextureManager.getPortraitTexture(), _portraitPosition, Color.White);
            _health.draw(sb);
            _energy.draw(sb);
            _boost.draw(sb);
            _btnLMB.draw(sb);
            _btnRMB.draw(sb);
            _killCounter.Draw(sb);
            _secondsLeft.Draw(sb);
        }

        internal void addDebugButton(string text) {
            if (_debugButtons == null) {
                _debugButtons = new List<ShortcutButton>();
            }

            var bounds = SmashTVSystem.Instance.Arena.Bounds;
            int row = _debugButtons.Count / 10;
            var y = bounds.Top + (_debugButtons.Count % 10 * 33);
            var x = bounds.Right + 20 + (row * 230);

            var button = new ShortcutButton(new Rectangle(x, y, 230, 30));
            button.setText(text);
            _debugButtons.Add(button);
        }

        public void setSecondsLeft(float time) {
            if (Utils.valueBetween((int)time, 0, 9)) {
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
    }
}

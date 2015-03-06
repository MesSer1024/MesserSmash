using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Commands;

namespace MesserSmash.GUI {
    public class NewUserScreen /*: IScreen */{
        /*
        private Rectangle _background;
        private StringBuilder _name;
        private bool _error = false;

        public NewUserScreen() {
            _name = new StringBuilder();
            _background = new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight());
        }

        public void update(float deltatime) {
            foreach (var key in Utils.getPressedKeys()) {
                if (Utils.isNewKeyPress(Keys.Back)) {
                    _error = false;
                    if (_name.Length > 0) {
                        _name.Remove(_name.Length - 1, 1);
                    }
                } else if (Utils.isNewKeyPress(Keys.Enter)) {
                    performRegisterUsername();
                } else {
                    if (validUsernameCharacter(key) && Utils.isNewKeyPress(key)) {
                        _error = false;
                        _name.Append(Utils.keyOutputCharacter(key));
                    }
                }
            }
        }

        private bool validUsernameCharacter(Keys key) {
            return (key >= Keys.A && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9) || (key >= Keys.D0 && key <= Keys.D9) || key == Keys.Space;
        }

        private void performRegisterUsername() {
            if (_name.Length > 2) {
                new RegisterUsernameCommand(_name.ToString()).execute();
            } else {
                _error = true;
            }
        }

        public void draw(SpriteBatch sb) {
            sb.Draw(AssetManager.getDefaultTexture(), _background, Color.Black);
            {
                var text = new FunnyText(Utils.makeString("Enter your username: {0}", _name.ToString()), new Rectangle { X = 0, Y = 0, Width = Utils.getGameWidth(), Height = Utils.getGameHeight() });
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
            }
            if (_error) {
                var text2 = new FunnyText(Utils.makeString("invalid username, must be 3+ characters long"), new Rectangle { X = 0, Y = 100, Width = Utils.getGameWidth(), Height = Utils.getGameHeight() });
                text2.HorizontalCenter = true;
                text2.Draw(sb);
            }
        }

        public void initialize() {
            //throw new NotImplementedException();
        }

        public void destroy() {
            //throw new NotImplementedException();
        }
         * */
    }
}

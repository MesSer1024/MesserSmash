using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash.GUI {
    class ShortcutButton {
        private Rectangle _innerBounds;
        private Rectangle _outerBounds;
        public Rectangle Bounds {
            get { return _outerBounds; }
            set { 
                _outerBounds = value;
                recalculateInnerBounds();
            }
        }

        private Color _borderColor;
        private Color _activeColor;
        private Color _inactiveColor;
        private bool _active;
        private string _text;

        public ShortcutButton(Rectangle bounds) {
            Bounds = bounds;
            _borderColor = Color.Black;
            _inactiveColor = Color.LightGray;
            _activeColor = Color.Yellow;
            _text = "";
        }

        private void recalculateInnerBounds() {
            int padding = 3;
            _innerBounds = new Rectangle(_outerBounds.Left + padding, _outerBounds.Top + padding, _outerBounds.Width - 2*padding, _outerBounds.Height - 2*padding);
        }

        public void draw(SpriteBatch sb) {
            var texture = AssetManager.getDefaultTexture();
            Color borderColor = _active ? _activeColor : _borderColor;
            Color innerColor = _active ? Color.White : _inactiveColor;
            sb.Draw(texture, _outerBounds, borderColor);
            sb.Draw(texture, _innerBounds, innerColor);

            if (hasText()) {
                sb.DrawString(AssetManager.getDefaultFont(), _text, new Vector2(_innerBounds.X + 1, _innerBounds.Y + 1), Color.Black);
            }
        }

        private bool hasText() {
            return _text != "";
        }

        public void setMode(bool active) {
            _active = active;
        }

        public void setText(string text) {
            _text = text;
        }
    }
}

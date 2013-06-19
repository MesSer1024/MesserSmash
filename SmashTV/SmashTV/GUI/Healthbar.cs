using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash.GUI {
    public class Healthbar {
        private Rectangle _innerBounds;
        private Rectangle _outerBounds;
        public Rectangle Bounds {
            get { return _outerBounds; }
            set { 
                _outerBounds = value;
                _innerBounds = new Rectangle(_outerBounds.X + 1, _outerBounds.Y + 1, _outerBounds.Width - 2, _outerBounds.Height - 2);
            }
        }

        /// <summary>
        /// Value between 0 and 1
        /// </summary>
        private float _value;
        public float Value {
            get { return _value; }
            set { _value = value; }
        }

        public Color _borderColor;
        public Color _nonValuedColor;
        public Color _valueColor;

        public Healthbar(Rectangle bounds) {
            Bounds = bounds;
            _value = 0.5f;
            _borderColor = Color.LightGray;
            _nonValuedColor = Color.Black;
            _valueColor = Color.Yellow;
        }

        public void draw(SpriteBatch sb) {
            sb.Draw(AssetManager.getDefaultTexture(), _outerBounds, _borderColor);
            sb.Draw(AssetManager.getDefaultTexture(), _innerBounds, _nonValuedColor);
            sb.Draw(AssetManager.getDefaultTexture(), getBoundsByValue(), _valueColor);            
        }

        private Rectangle getBoundsByValue() {
            double valueWidth = _innerBounds.Width * _value;
            return new Rectangle(_innerBounds.X, _innerBounds.Y, (int)valueWidth, _innerBounds.Height);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash.GUI {
    class FunnyText {
        public Color TextColor = Color.OrangeRed;
        public bool Visible;
        private bool _VerticalCenter = true;

        public bool VerticalCenter {
            get { return _VerticalCenter; }
            set { _VerticalCenter = value;
            updatePosition();
            }
        }
        private bool _HorizontalCenter = true;

        public bool HorizontalCenter {
            get { return _HorizontalCenter; }
            set { _HorizontalCenter = value;
            updatePosition();
            }
        }

        private float _textScale = 1;
        public float TextScale {
            set { _textScale = value;
            updatePosition();
            }
        }

        private Rectangle _bounds;
        private Vector2 _innerBounds;
        private SpriteFont _font;        
        private string _text;

        public string Text {
            get { return _text; }
            set {
                _text = value;
                updatePosition();
            }
        }

        public int X {
            get { return _bounds.X; }
            set { _bounds.X = value; }
        }

        public int Y {
            get { return _bounds.Y; }
            set { _bounds.Y = value; }
        }

        public Rectangle Bounds {
            get { return _bounds; }
            set { _bounds = value; }
        }

        public FunnyText(String s, Rectangle bounds) {
            _font = AssetManager._bigGuiFont;
            _bounds = bounds;
            Visible = true;
            _text = s;
            updatePosition();
        }

        private void updatePosition() {
            Vector2 foo = _font.MeasureString(_text) * _textScale;
            var w = _bounds.Width - foo.X;
            var h = _bounds.Height - foo.Y;
            if (VerticalCenter && HorizontalCenter) {
                _innerBounds = new Vector2(w / 2 + _bounds.X, h / 2 + _bounds.Y);
            } else if (VerticalCenter) {
                _innerBounds = new Vector2(_bounds.X, _bounds.Y + h/2);
            } else if (HorizontalCenter) {
                _innerBounds = new Vector2(_bounds.X + w/2, _bounds.Y);
            } else {
                _innerBounds = new Vector2(_bounds.X, _bounds.Y);
            }
        }

        public void Draw(SpriteBatch sb) {
            if (!Visible) {
                return;
            }
            
            sb.DrawString(_font, _text, _innerBounds, TextColor, 0, Vector2.Zero, _textScale, SpriteEffects.None, 0);
        }
    }
}

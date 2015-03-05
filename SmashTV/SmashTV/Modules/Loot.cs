using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MesserSmash.Arenas;

namespace MesserSmash {
    public class Loot {
        private Texture2D _healthTexture;
        private Texture2D _moneyTexture;
        private Arena.LootType _type;

        public Arena.LootType Type {
            get { return _type; }
        }

        private Texture2D _texture;
        private float _timeUntilRemoval;

        private bool _isActive;
        public bool IsActive {
            get { return _isActive; }
        }

        private Vector2 _position;

        public Vector2 Position {
            get { return _position; }
        }
        private float _radius;

        public float Radius {
            get { return _radius; }
        }
        private float _scale;
        private int _textureOrigin;
        private float _originalTime;

        public Loot(MesserSmash.Arenas.Arena.LootType type, Texture2D healthTexture, Texture2D moneyTexture, Vector2 position) {
            _type = type;
            _healthTexture = healthTexture;
            _moneyTexture = moneyTexture;
            _isActive = true;
            _position = position;
            _radius = 38 * Utils.getResolutionScale();

            switch (_type) {
                case Arena.LootType.Health:
                    _timeUntilRemoval = 12;
                    _texture = _healthTexture;
                    break;
                case Arena.LootType.Money:
                    _timeUntilRemoval = 10;
                    _texture = _moneyTexture;
                    break;
            }
            _originalTime = _timeUntilRemoval;
            _scale = 2 * _radius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
        }

        public void update(float gametime) {
            if (_isActive) {
                _timeUntilRemoval -= gametime;
                if (_timeUntilRemoval <= 0) {
                    _isActive = false;
                }
            }
        }

        public void draw(SpriteBatch sb) {
            if (_isActive) {
                Color color = Color.White;
                if (_type == Arena.LootType.Health) {
                    color = Color.PapayaWhip;
                } else {
                    color = Color.WhiteSmoke;
                }

                float alpha = _timeUntilRemoval / _originalTime;
                if (alpha <= 0.1f && alpha > 0.08f) {
                    color = Color.White;
                } else if (alpha <= 0.08f && alpha > 0.06f) {
                    color.A = 0;
                }else if (alpha < 0.5f ) {
                    color.A = (byte)(color.A * alpha);
                }
                sb.Draw(_texture, _position, null, color, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
            }
        }

        public void inactivate() {
            _isActive = false;
        }
    }
}

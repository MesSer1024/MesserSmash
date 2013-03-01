using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash {
    class Explosion {
        private float _timeToShow;
        private float _currTime;
        private Vector2 _position;
        private float _radius;
        
        private Texture2D _texture;
        private float _scale;
        private int _textureOrigin;
        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } }

        public Explosion(Vector2 pos, float radius, Texture2D texture, float timeToShow) {
            _position = pos;
            _radius = radius;
            _texture = texture;
            _timeToShow = timeToShow;

            _scale = 2 * _radius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
            _currTime = 0;
            _isAlive = true;
        }

        public void update(float deltatime) {
            _currTime += deltatime;
            if (_isAlive) {
                if (_currTime >= _timeToShow) {
                    _isAlive = false;
                }
            }
        }

        public void draw(SpriteBatch sb) {
            if(_isAlive) {
                Color color = Color.DarkBlue;
                sb.Draw(_texture, _position, null, Color.DarkBlue, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
            }
        }
    }
}

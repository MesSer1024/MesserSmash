using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash {
    class PistolShot : ShotBase {
        private const float MOVEMENT_SPEED = 723;
        public enum ShotStates {
            Alive,
            Dead,
            Removed,
        }

        private ShotStates _state;
        public ShotStates State {
            get { return _state; }
        }

        protected override bool _flaggedForCollision() {
            return _state == ShotStates.Alive;
        }

        protected override bool _removable() {
            return _state == ShotStates.Removed;
        }
 
        public PistolShot(Vector2 position, Vector2 direction) {
            _position = position;
            _velocity = direction;
            if(_velocity == Vector2.Zero) 
                _velocity = new Vector2(1,0);
            _velocity.Normalize();
            _velocity *= MOVEMENT_SPEED;
            
            _damage = 3;
            _state = ShotStates.Alive;            
        }

        protected override float _initRadius() {
            return 5;
        }

        protected override Texture2D _initTexture() {
            return AssetManager.getShotTexture();
        }

        protected override void _update(float deltatime) {
            if (_state != ShotStates.Removed) {
                if (_state == ShotStates.Dead) {
                    if (_timeInState > 3.5f) {
                        _state = ShotStates.Removed;
                    }
                } else {
                    _position += _velocity * deltatime;
                }
            }
        }

        protected override void _draw(SpriteBatch sb) {
            if (_state != ShotStates.Removed) {
                if (_state == ShotStates.Dead) {
                    sb.Draw(_texture, _position, null, Color.Red, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
                } else {
                    sb.Draw(_texture, _position, null, Color.Yellow, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
                }
            }
        }

        protected override void _entityCollision(Vector2 impactPosition) {
            if (_state == ShotStates.Removed || _state == ShotStates.Dead)
                return;
            _state = ShotStates.Dead;
            _timeInState = 0;
            _position = impactPosition;
        }
    }
}
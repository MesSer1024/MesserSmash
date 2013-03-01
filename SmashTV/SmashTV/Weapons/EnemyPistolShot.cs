// -----------------------------------------------------------------------
// <copyright file="EnemyPistolShot.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Weapons {

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnemyPistolShot : ShotBase {
        private const float MOVEMENT_SPEED = 282;
        private enum ShotStates {
            Alive,
            Dead,
            Removed,
        }

        private ShotStates _state = ShotStates.Alive;

        protected override bool _flaggedForCollision() {
            return _state == ShotStates.Alive;
        }

        protected override bool _removable() {
            return _state == ShotStates.Removed;
        }

        public EnemyPistolShot(Vector2 position, Vector2 direction) : base(position, true){
            _position = position;
            _velocity = Utils.safeNormalize(direction);
            _velocity *= MOVEMENT_SPEED;
            
            _damage = 7;
        }

        protected override float _initRadius() {
            return 3;
        }

        protected override Texture2D _initTexture() {
            return TextureManager.getShotTexture();
        }

        protected override bool _doesSplashDamage() {
            return false;
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

        protected override void _explode(Vector2 impactPosition) {
            if (_state == ShotStates.Removed || _state == ShotStates.Dead)
                return;
            _state = ShotStates.Dead;
            _timeInState = 0;
            _position = impactPosition;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Modules;
using MesserSmash.Commands;

namespace MesserSmash {
    class RocketShot : ShotBase {
        private const float GROUND_SIZE = 132;
        private const float FLIGHT_SIZE = 8;

        private const float FLYING_TIME = 1.35f;
        private const float EXPLOSION_TIME_SHOWN = 0.750f;
        private const float EXPLOSION_TIME_DEAL_DAMAGE = 0.175f;        
        private readonly float MOVEMENT_SPEED;
        private Vector2 _direction;
        private Vector2 _targetPosition;



        private enum ShotState {
            Flying,
            Landed,
            ExplodeAnimation,
            Removable,
        }
        private ShotState _state;

        public RocketShot(Vector2 orgPos, Vector2 targetPosition) {
            _position = orgPos;
            _targetPosition = targetPosition;
            _direction = targetPosition - orgPos;
            MOVEMENT_SPEED = _direction.Length() / FLYING_TIME;
            if (_direction == Vector2.Zero) {
                _direction = new Vector2(1, 0);
            }
            _direction.Normalize();
            _state = ShotState.Flying;
            _damage = 12;
        }

        protected override float _initRadius() {
            return FLIGHT_SIZE * Utils.getResolutionScale();
        }

        protected override Texture2D _initTexture() {
            return AssetManager.getRocketShotTexture();
        }

        protected override bool _flaggedForCollision() {
            return _state == ShotState.Landed;
        }

        protected override bool _removable() {
            return _state == ShotState.Removable;
        }

        protected override void _entityCollision(Vector2 impactPosition) {
            //do nothing on hit with unit since we should not change this shots behavior in that scenario
        }

        protected override void _update(float gametime) {
            switch (_state) {
                case ShotState.Flying:
                    //calculate where to draw me and check for treshold so it can switch to state LANDED
                    _velocity = _direction * MOVEMENT_SPEED * gametime;

                    if (isOutsideOfBounds()) {
                        //_position += _velocity;
                        new BoundaryChecker().moveToWall(this, _direction);
                        land();
                    } else {
                        _position += _velocity;
                        if (_timeInState >= FLYING_TIME) {
                            land();
                        }
                    }
                    break;
                case ShotState.Landed:
                    if (_timeInState >= EXPLOSION_TIME_DEAL_DAMAGE) {
                        _state = ShotState.Removable;
                    }
                    break;
            }
        }

        private bool isOutsideOfBounds() {
            return new BoundaryChecker().entityWasOutsideBoundsBeforeRestriction(this);
        }
        
        private void land() {
            new PlaySoundCommand(AssetManager.getGrenadeImpactSound()).execute();
            _timeInState = 0;
            _state = ShotState.Landed;
            _radius = GROUND_SIZE * Utils.getResolutionScale();            
            requestBecomeGroundEffect(EXPLOSION_TIME_DEAL_DAMAGE, EXPLOSION_TIME_SHOWN);
        }

        protected override void _draw(SpriteBatch sb) {
            float scale = _scale;
            Color color = Color.DarkOrchid;
            switch (_state) {
                case ShotState.Flying:
                    float halfTime = FLYING_TIME / 2;
                    float k = 1 / halfTime;
                    if (_timeInState <= halfTime) {
                        //y=kx
                        scale = _timeInState * k;
                    } else {
                        //y= (deltaX)*k
                        scale = (FLYING_TIME - _timeInState) * k;
                    }
                    sb.Draw(_texture, _position, null, color, 0f, new Vector2(_textureOrigin, _textureOrigin), scale, SpriteEffects.None, 0);
                    break;
            }
        }
    }
}

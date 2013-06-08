using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Enemies;
using MesserSmash.Modules;
using MesserSmash.Commands;

namespace MesserSmash {
    public class Player : IEntity {
        private const float TOTAL_HURT_ANIM_TIME = 0.155f;
        private Texture2D _texture;
        private Vector2 _position;

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _velocity;
        private float _scale;
        private float _radius;

        public float Radius {
            get { return _radius; }
        }
        private int _textureOrigin;

        private WeaponPistol _weaponLMB;
        private WeaponRocketLauncher _weaponRMB;

        private float _health;
        public float Health {
            get { return _health; }
        }

        public bool CollisionEnabled { get { return true; } }

        private PlayerState _currentState;
        private bool _soundlock;
        private bool _finished;
        private float _takeDamageAnimationTime;        

        public Player(Vector2 position) {
            _position = position;
            _texture = AssetManager.getPlayerTexture();
            _velocity = Vector2.Zero;
            _radius = 18;
            _scale = 2 * _radius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
            _weaponLMB = new WeaponPistol();
            _weaponRMB = new WeaponRocketLauncher();
            _health = 100;
            _currentState = new NormalState(this);
            _takeDamageAnimationTime = 1500;
        }

        public void update(float deltatime) {
            _weaponLMB.update(deltatime);
            _weaponRMB.update(deltatime);
            _velocity = Vector2.Zero;

            //the state of the player affects movement and handles state specific inputs!
            _currentState.validate(deltatime);
            _currentState.handleInput();
            if (_soundlock && !Utils.RmbPressed) {
                _soundlock = false;
            }
            Vector2 movement = Utils.safeNormalize(generateMovementVector());
            _velocity = movement * _currentState.getMovementSpeed() * deltatime;
            restrictMovementToBounds();
            moveUnit(deltatime);
            _takeDamageAnimationTime += deltatime;
        }

        private Vector2 generateMovementVector() {
            var pressedKeys = Utils.getPressedKeys();
            Vector2 value = Vector2.Zero;

            foreach (Keys key in pressedKeys) {
                if (key == Keys.W) {
                    value.Y -= 1;
                } else if (key == Keys.S) {
                    value.Y += 1;
                } else if (key == Keys.A) {
                    value.X -= 1;
                } else if (key == Keys.D) {
                    value.X += 1;
                }
            }
            return value;
        }

        private void restrictMovementToBounds() {
            new BoundaryChecker().restrictMovementToBounds(this);
        }

        private void moveUnit(float deltatime) {
            if (isSprinting() && _velocity != Vector2.Zero) {
                if (EnergySystem.Instance.couldRun(deltatime) == false)
                    return;
            }
            _position += _velocity;
        }

        private bool isSprinting() {
            return _currentState is SprintState;
        }

        public void draw(SpriteBatch sb) {
            var color = Color.Yellow;
            if (_takeDamageAnimationTime < TOTAL_HURT_ANIM_TIME) {
                var t = TOTAL_HURT_ANIM_TIME / 2;
                if (_takeDamageAnimationTime < t) {
                    color.G = (byte)(byte.MaxValue - (_takeDamageAnimationTime / t * byte.MaxValue));
                } else {
                    color.G = (byte)(((_takeDamageAnimationTime - t) / t) * byte.MaxValue);
                }
            }
            sb.Draw(_texture, _position, null, color, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
        }

        public void takeDamage(float amount) {
            if (_finished) {
                return;
            }

            _takeDamageAnimationTime = 0;
            _health -= amount;
            if (_health <= 0) {
                if (DataDefines.ID_SETTINGS_GOD_MODE != 0) {
                    return;
                }
                _health = 0;
                new PlayerDiedCommand(this).execute();
                //player is dead...
                //throw new Exception("Player is dead...");
            }
        }

        public void receiveHealth(float amount) {
            _health += amount;
        }

        public void tryChangeState(PlayerState state) {
            _currentState = state;
        }

        public void stateEndGame() {
            _finished = true;
        }

        public void tryFireLMB() {
            if(_weaponLMB.canFireShot()) {
                _weaponLMB.tryFireShot(_position, Utils.getMousePos());
            }
        }

        public void tryFireRMB() {
            if (_weaponRMB.noCooldown()) {
                if (EnergySystem.Instance.canFireRocket()) {
                    _weaponRMB.tryFireShot(_position, Utils.getMousePos());
                    _soundlock = false;
                }
                else {
                    if (_soundlock == false) {
                        _soundlock = true;
                        new PlaySoundCommand(AssetManager.getFailSound()).execute();
                    }
                }
            }
        }


        public Vector2 Velocity {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public bool IsAlive {
            get { return _health > 0; }
        }


        public void die(bool notify) {
            if (notify) {
                takeDamage(_health);
            } else {
                _health = 0;                
            }
        }
    }
}
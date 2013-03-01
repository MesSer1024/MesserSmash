using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Enemies;
using MesserSmash.Modules;

namespace MesserSmash {
    public class Player : IEntity {
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
        private int _money;
        public int Money {
            get { return _money; }
        }

        private PlayerState _currentState;

        public Player(Vector2 position) {
            _position = position;
            _texture = TextureManager.getPlayerTexture();
            _velocity = Vector2.Zero;
            _radius = 18;
            _scale = 2 * _radius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
            _weaponLMB = new WeaponPistol();
            _weaponRMB = new WeaponRocketLauncher();
            _money = 0;
            _health = 100;
            _currentState = new NormalState(this);
        }

        public void update(float deltatime) {
            _weaponLMB.update(deltatime);
            _weaponRMB.update(deltatime);
            _velocity = Vector2.Zero;

            //the state of the player affects movement and handles state specific inputs!
            _currentState.validate(deltatime);
            _currentState.handleInput();
            Vector2 movement = Utils.safeNormalize(generateMovementVector());
            _velocity = movement * _currentState.getMovementSpeed() * deltatime;
            restrictMovementToBounds();
            moveUnit(deltatime);
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
            sb.Draw(_texture, _position, null, Color.Yellow, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale, SpriteEffects.None, 0);
        }

        public void takeDamage(float amount) {
            _health -= amount;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.PlayerDamaged, this, "damage:" + amount + "|health=" + _health);
            if (_health <= 0) {
                EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.PlayerDead, this, "damage:" + amount + "|health=" + _health);
                _health = 0;
                //player is dead...
                //throw new Exception("Player is dead...");
            }
        }

        public void receiveHealth(float amount) {
            _health += amount;
        }

        public void receiveMoney(int amount) {
            _money += amount;
        }

        public void tryChangeState(PlayerState state) {
            _currentState = state;
        }

        public void stateEndGame() {

        }

        public void tryFireLMB() {
            if(_weaponLMB.canFireShot()) {
                _weaponLMB.tryFireShot(_position, Utils.getMousePos());
            }
        }

        public void tryFireRMB() {
            if (_weaponRMB.canFireShot()) {
                _weaponRMB.tryFireShot(_position, Utils.getMousePos());
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
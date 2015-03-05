using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Enemies;
using Microsoft.Xna.Framework.Audio;

namespace MesserSmash {
    public abstract class ShotBase : IEntity {
        public delegate void ShotDelegate(ShotBase shot, float timeDealDamage, float timeToShow);
        public event ShotDelegate GenerateGroundEffect;

        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Texture2D _texture;
        protected float _scale;
        protected float _radius;
        public float Radius {
            get { return _radius; }
        }
        protected int _textureOrigin;
        protected float _timeInState;
        protected float _damage;

        public float Damage {
            get { return _damage; }
        }

        public UInt16 Identifier { get; private set; }
        public bool CollisionEnabled { get { return _flaggedForCollision(); } }

        public ShotBase() : this(Vector2.Zero, false) {}

        public ShotBase(Vector2 position, bool firedByEnemy=true) {
            Identifier = Utils.NextIdentifier;
            _position = position;
            _texture = _initTexture();

            _velocity = Vector2.Zero;
            _timeInState = 0;
            _damage = 0;
            _radius = _initRadius();
            _scale = 2 * _radius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
            if (firedByEnemy) {
                SmashTVSystem.Instance.ShotContainer.addEnemyShot(this);
            } else {
                SmashTVSystem.Instance.ShotContainer.addShot(this);
            }
        }

        protected abstract Texture2D _initTexture();
        protected abstract float _initRadius();
        protected abstract bool _flaggedForCollision();
        protected abstract bool _removable();

        public void update(float deltatime) {
            _timeInState += deltatime;
            _update(deltatime);
        }
        protected abstract void _update(float deltatime);

        public void draw(SpriteBatch sb) {
            _draw(sb);
        }
        protected abstract void _draw(SpriteBatch sb);

        public void entityCollision(Vector2 impactPosition) {
            _entityCollision(impactPosition);
        }
        protected abstract void _entityCollision(Vector2 impactPosition);

        protected void requestBecomeGroundEffect(float timeDealDamage, float timeToShow) {
            if (GenerateGroundEffect != null) {
                GenerateGroundEffect.Invoke(this, timeDealDamage, timeToShow);
            }
        }

        public Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
            }
        }

        public Vector2 Velocity {
            get {
                return _velocity;
            }
            set {
                _velocity = value;
            }
        }

        public bool IsAlive {
            get { return _removable(); }
        }


        public void die(bool runDeathlogic) {
            
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Commands;
using MesserSmash.Modules;
using SharedSmashResources.Patterns;

namespace MesserSmash.Enemies {
    public class EnemyBase : IEnemy {
        #region Enum and EventHandlers

        public enum EnemyStates {
            EngagingPlayer,
            Dead,
            Removed,
            Attacking,
            Leaving
        }

        #endregion Enum and eventHandlers

        #region Class Members
        public delegate void EnemyDelegate(IEnemy enemy);

        private float _attackRadiusScale;
        private float _scale;
        private Texture2D _texture;
        private int _textureOrigin;
        private float _health;
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected float _radius;
        protected Player _target;

        private EnemyStates _state;
        protected Behaviour _behaviour;
        private static int _identifier;
        private int _id;

        public float TimeSinceCreation { get; protected set; }

        public float Health {
            get { return _health; }
            set { _health = value; }
        }

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Velocity {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public float AttackRadius { get; protected set; }
        public float Damage { get; protected set; }
        public float Score { get; protected set; }

        public EnemyStates State {
            get { return _state; }
            protected set {
                _state = value;
                TimeSinceCreation = 0;
                handleStateChange();
            }
        }

        public float Radius {
            get { return _radius; }
        }
        #endregion Class Members

        #region constructors and initializer methods
        public EnemyBase() {
            SmashTVSystem.Instance.enemyCreated();
            Damage = 20;
            _id = _identifier++;

            reloadDatabaseValues();
        }

        public void init() {
            //Controller.instance.addObserver(this);
            SmashTVSystem.Instance.EnemyContainer.addEnemy(this);
            State = EnemyStates.EngagingPlayer;
        }

        public void reloadDatabaseValues() {
            _texture = _getTexture();
            _radius = _getRadius();
            AttackRadius = _getAttackRadius();
            _scale = 2 * _radius / _texture.Width;
            _attackRadiusScale = 2 * AttackRadius / _texture.Width;
            _textureOrigin = _texture.Width / 2;
            Score = _getScore();
        }

        protected virtual float _getMovementSpeed() {
            return 50;
        }

        protected virtual float _getAttackRadius() {
            return _radius * 2;
        }

        protected virtual Texture2D _getTexture() {
            throw new NotImplementedException("SubClass responsibility");
        }

        protected virtual float _getRadius() {
            throw new NotImplementedException("SubClass responsibility");
        }

        protected virtual float _getScore() {
            throw new NotImplementedException("SubClass responsibility");
        }
        #endregion constructors and initializer methods

        public virtual bool IsAlive {
            //handle all non-dead-states
            get { return State != EnemyStates.Dead && State != EnemyStates.Removed; }
        }

        public virtual bool CollisionEnabled {
            //handle all non-dead-states
            get { return _behaviour != null && _behaviour.PathFindEnabled; }
        }

        public void preUpdate(float deltatime) {
            TimeSinceCreation += deltatime;

            //it is up to the behaviour to validate that the target position is valid and functioning (inside bounds etc)
            if (IsAlive) {
                _behaviour.Position = _position;
                _behaviour.update(deltatime);
                _velocity = _behaviour.Velocity;
            } else {
                _velocity = Vector2.Zero;
                _behaviour.update(deltatime);
            }

            //update(deltatime);
        }

        public virtual void drawBegin(SpriteBatch sb) {
            if (DataDefines.ID_SETTINGS_DRAW_ATTACK_RADIUS != 0 && IsAlive) {
                sb.Draw(_texture, _position, null, getAttackRadiusColor() * 0.55f, 0f,
                        new Vector2(_textureOrigin, _textureOrigin), _attackRadiusScale, SpriteEffects.None, 0);
            }
            if (State == EnemyStates.Dead) {
                sb.Draw(_texture, _position, null, Color.Red, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale,
                        SpriteEffects.None, 0);
            }
        }

        public virtual void draw(SpriteBatch sb) {
            if (State == EnemyStates.Removed || State == EnemyStates.Dead) {
                return;
            }

            Color color = getColor();
            sb.Draw(_texture, _position, null, color, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale,
                    SpriteEffects.None, 0);
        }

        public virtual void takeDamage(float amount) {
            if (State == EnemyStates.Dead || State == EnemyStates.Removed) {
                return;
            }
            _health -= amount;
            if (_health <= 0) {
                State = EnemyStates.Dead;
                notifyEnemyDied(amount);
            }
        }

        public virtual void onPlayerInAttackRadius() {
            State = EnemyStates.Attacking;
        }

        public virtual void onArenaEnded() {
            State = EnemyStates.Leaving;
        }

        private void onDeadDone(Behaviour behaviour) {
            State = EnemyStates.Removed;
        }

        protected void onAttackDone(Behaviour behaviour) {
            State = EnemyStates.EngagingPlayer;
        }

        private void onLeavingDone(Behaviour behaviour) {
            State = EnemyStates.Removed;
        }

        protected void notifyEnemyDied(float amount = 0) {
            new DeadEnemyCommand(this, amount).execute();
        }

        public virtual void update(float deltatime) {
            _position += _velocity;
        }

        protected virtual Color getAttackRadiusColor() {
            return Color.White;
        }

        protected virtual Color getColor() {
            return Color.Navy;
        }

        protected virtual void handleStateChange() {
            switch (State) {
                case EnemyStates.EngagingPlayer:
                    _behaviour = createEngagingBehaviour();
                    break;
                case EnemyStates.Attacking:
                    _behaviour = createAttackBehaviour();
                    break;
                case EnemyStates.Dead:
                    _behaviour = createDeadBehaviour();
                    break;
                case EnemyStates.Leaving:
                    _behaviour = createLeavingBehaviour();
                    break;
                default:
                    _behaviour = new NullBehaviour();
                    break;
            }
            updateBehaviour(_behaviour);
        }

        //called when unit is in engaging and player enters their attack radius

        protected virtual Behaviour createEngagingBehaviour() {
            return new EngagingMeleeUnitBehaviour(_getMovementSpeed());
        }

        protected virtual Behaviour createAttackBehaviour() {
            var behavior = new NullBehaviour();
            behavior.onBehaviourEnded += onAttackDone;
            return behavior;
        }

        protected virtual Behaviour createDeadBehaviour() {
            var behaviour = new DeadBehaviour();
            behaviour.onBehaviourEnded += onDeadDone;
            return behaviour;
        }

        private Behaviour createLeavingBehaviour() {
            var behaviour = new LeavingBehaviour();
            behaviour.onBehaviourEnded += onLeavingDone;
            return behaviour;
        }

        private void updateBehaviour(Behaviour behaviour) {
            behaviour.Target = _target;
            behaviour.Position = _position;
            behaviour.Enemy = this;
            _behaviour = behaviour;
        }

        public void die(bool runDeathLogic) {
            State = EnemyStates.Dead;
            handleStateChange();
        }

        ~EnemyBase() {
            SmashTVSystem.Instance.enemyRemoved();
        }
    }
}
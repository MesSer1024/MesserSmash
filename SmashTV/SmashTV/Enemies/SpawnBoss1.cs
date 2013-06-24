using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.GUI;
using MesserSmash.Commands;

namespace MesserSmash.Enemies {
    class SpawnBoss1 : EnemyBase {
        public delegate void Boss1Delegate(SpawnBoss1 sender);
        public event Boss1Delegate OnDamaged;

        private const float SECONDS_ENGAGING_BEFORE_CHARGE = 1.750f;
        private const float SECONDS_BEFORE_FIRST_ATTACK = 1.575f;
        private const float SECONDS_SLEEPING_AFTER_CHARGE = 2.250f;
        private const float MAX_HEALTH = 1650f;
        private bool _hasDoneFirstAttack;
        private Healthbar _healthbar;
        private Healthbar _energyBar;
        private float MAX_CHARGE_TIME = 1.10f;
        public float ScoreWhenHit { get { return 3; } }

        public SpawnBoss1(Vector2 position, Player player) {
            Position = position;
            Damage = 35;
            _target = player;
            Health = MAX_HEALTH;
            _hasDoneFirstAttack = false;

            _healthbar = new Healthbar(new Rectangle(0, 0, (int)(150 * Utils.getResolutionScale()), (int)(150 * Utils.getResolutionScale())));
            _energyBar = new Healthbar(new Rectangle(0, 20, (int)(150 * Utils.getResolutionScale()), (int)(150 * Utils.getResolutionScale())));
            _energyBar._nonValuedColor = Color.Black;
            _energyBar._valueColor = Color.LightSteelBlue;
            _energyBar.Value = 1;
            _healthbar.Value = 1;
        }

        protected override float _getMovementSpeed() {
            return 85 * Utils.getResolutionScale();
        }

        protected override Color getColor() {
            return Color.Yellow;
        }

        protected override Color getAttackRadiusColor() {
            return Color.PaleGoldenrod;
        }

        protected override float _getRadius() {
            return 75 * Utils.getResolutionScale();
        }

        protected override float _getAttackRadius() {
            return 406 * Utils.getResolutionScale();
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getEnemyTexture();
        }

        protected override float _getScore() {
            return 1875;
        }

        protected override void onInit() {
            State = EnemyStates.Idle;
            _behaviour = new NullBehaviour();
        }

        public override void drawBegin(SpriteBatch sb) {
            if (IsAlive) {
                sb.Draw(_getTexture(), _position, null, getAttackRadiusColor() * 0.27f, 0f,
                        new Vector2(_textureOrigin, _textureOrigin), _attackRadiusScale, SpriteEffects.None, 0);
            }
            if (State == EnemyStates.Dead) {
                sb.Draw(_getTexture(), _position, null, Color.Red, 0f, new Vector2(_textureOrigin, _textureOrigin), _scale,
                        SpriteEffects.None, 0);
            }
        }

        public override void draw(SpriteBatch sb) {
            base.draw(sb);
            if (IsAlive) {
                _healthbar.draw(sb);
            }

            switch (State) {
                case EnemyStates.Idle:
                    _energyBar.Value = _behaviour.TimeThisBehaviour / SECONDS_SLEEPING_AFTER_CHARGE;
                    _energyBar.draw(sb);
                    break;
                case EnemyStates.EngagingPlayer:
                    _energyBar.Value = 1;
                    _energyBar.draw(sb);
                    break;
                case EnemyStates.Attacking:
                    _energyBar.Value = (MAX_CHARGE_TIME - _behaviour.TimeThisBehaviour) / MAX_CHARGE_TIME;
                    _energyBar.draw(sb);
                    break;
            }
        }

        override public void onPlayerInAttackRadius() {
            if (!_hasDoneFirstAttack && TimeSinceCreation > SECONDS_BEFORE_FIRST_ATTACK) {
                _hasDoneFirstAttack = true;
                State = EnemyStates.Attacking;
            }
            if (_behaviour.TimeThisBehaviour > SECONDS_ENGAGING_BEFORE_CHARGE) {
                State = EnemyStates.Attacking;
            }
        }

        protected override Behaviour createAttackBehaviour() {
            new PlaySoundCommand(AssetManager.getEnemyChargeSound()).execute();
            var behaviour = new AttackWithCharge(_getMovementSpeed(), _getMovementSpeed() * 5.15f, MAX_CHARGE_TIME);
            behaviour.onBehaviourEnded += onBossAttackDone;
            return behaviour;
        }

        private void onBossAttackDone(Behaviour behaviour) {
            State = EnemyStates.Idle;
            _behaviour = new NullBehaviour();
        }

        public override void update(float deltatime) {
            base.update(deltatime);
            _healthbar.Value = Health / MAX_HEALTH;
            var r = new Rectangle((int)_position.X - (int)(113 * Utils.getResolutionScale()), (int)_position.Y - 2 * _textureOrigin, (int)(225 * Utils.getResolutionScale()), 10);
            _healthbar.Bounds = r;
            r.Y = _healthbar.Bounds.Bottom;
            _energyBar.Bounds = r;
            if (State == EnemyBase.EnemyStates.Idle && _behaviour.TimeThisBehaviour > SECONDS_SLEEPING_AFTER_CHARGE) {
                State = EnemyBase.EnemyStates.EngagingPlayer;
            }
        }

        public override void takeDamage(float amount) {
            base.takeDamage(amount);
            if (OnDamaged != null) {
                OnDamaged.Invoke(this);
            }
        }
    }
}

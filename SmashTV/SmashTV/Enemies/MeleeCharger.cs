using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Modules;
using MesserSmash.Commands;

namespace MesserSmash.Enemies {

    public class MeleeCharger : EnemyBase {

        private const float SECONDS_ENGAGING_BEFORE_CHARGE = 1.975f;
        private const float SECONDS_BEFORE_FIRST_ATTACK = 1.975f;
        private const float SECONDS_SLEEPING_AFTER_CHARGE = 1.12f;

        private bool _hasDoneFirstAttack;

        public MeleeCharger(Vector2 position, Player player) {
            Position = position;
            Damage = DataDefines.ID_RUSHER_DAMAGE;
            _target = player;
            _hasDoneFirstAttack = false;
        }

        protected override float _getMovementSpeed() {
            return base._getMovementSpeed() - 21*Utils.getResolutionScale();
        }

        protected override Color getColor() {
            return Color.Yellow;
        }

        protected override float _getRadius() {
            return DataDefines.ID_RUSHER_RADIUS * Utils.getResolutionScale();
        }

        protected override float _getAttackRadius() {
            return DataDefines.ID_RUSHER_ATTACK_RADIUS * Utils.getResolutionScale();
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getEnemyTexture();
        }

        protected override float _getScore() {
            return DataDefines.ID_RUSHER_SCORE;
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
            var behaviour = new AttackWithCharge(_getMovementSpeed(), 285f * Utils.getResolutionScale(), 1.0f);
            behaviour.onBehaviourEnded += onChargeAttackDone;
            return behaviour;
        }

        private void onChargeAttackDone(Behaviour behaviour) {
            State = EnemyStates.Idle;
            _behaviour = new NullBehaviour();
        }

        public override void update(float deltatime) {
            base.update(deltatime);

            if (State == EnemyBase.EnemyStates.Idle && _behaviour.TimeThisBehaviour > SECONDS_SLEEPING_AFTER_CHARGE) {
                State = EnemyBase.EnemyStates.EngagingPlayer;
            }
        }
    }
}

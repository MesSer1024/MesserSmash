using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {

    public class MeleeStabber : EnemyBase {
        private const float SECONDS_BETWEEN_CHARGE = 2.5f;
        private const float SECONDS_BEFORE_FIRST_CHARGE = 0.30f;

        public MeleeStabber(Vector2 position, Player player) {
            Position = position;
            Damage = DataDefines.ID_RUSHER_DAMAGE;
            _target = player;
        }

        protected override float _getMovementSpeed() {
            return base._getMovementSpeed() - 10;
        }

        protected override Color getColor() {
            return Color.Yellow;
        }

        protected override float _getRadius() {
            return DataDefines.ID_RUSHER_RADIUS;
        }

        protected override float _getAttackRadius() {
            return DataDefines.ID_RUSHER_ATTACK_RADIUS;
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getEnemyTexture();
        }

        override public void onPlayerInAttackRadius() {
            if (TimeSinceCreation > SECONDS_BEFORE_FIRST_CHARGE && _behaviour.TimeThisBehaviour > SECONDS_BETWEEN_CHARGE) {
                State = EnemyStates.Attacking;
            }
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackWithCharge(_getMovementSpeed(), 180f, 1f);
            behaviour.onBehaviourEnded += onAttackDone;
            return behaviour;
        }
    }
}

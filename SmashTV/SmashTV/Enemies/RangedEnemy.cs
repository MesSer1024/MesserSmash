using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {
    class RangedEnemy : EnemyBase {
        public RangedEnemy(Vector2 position, Player player) {
            Position = position;
            _target = player;
            Damage = DataDefines.ID_RANGE_DAMAGE;
        }

        protected override float _getRadius() {
            return DataDefines.ID_RANGE_RADIUS;
        }

        protected override float _getAttackRadius() {
            return DataDefines.ID_RANGE_ATTACK_RADIUS;
        }

        protected override Texture2D _getTexture() {
            return TextureManager.getRangedEnemyTexture();
        }

        protected override Color getColor() {
            return Color.Beige;
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackingRangeBehaviour();
            behaviour.onBehaviourEnded += onAttackBehaviourEnded;
            return behaviour;
        }

        void onAttackBehaviourEnded(Behaviour behaviour) {
            State = EnemyStates.EngagingPlayer;
        }
    }
}

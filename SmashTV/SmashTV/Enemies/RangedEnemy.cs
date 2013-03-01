using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;

namespace MesserSmash.Enemies {
    class RangedEnemy : EnemyBase {
        public RangedEnemy(Vector2 position, Player player) {
            Position = position;
            _target = player;
            Damage = 7;
        }

        protected override float _getRadius() {
            return 11;
        }

        protected override float _getAttackRadius() {
            return 120;
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

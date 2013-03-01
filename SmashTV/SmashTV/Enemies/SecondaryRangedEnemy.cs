using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;

namespace MesserSmash.Enemies {
    public class SecondaryRangedEnemy : EnemyBase {
        public SecondaryRangedEnemy(Vector2 position, Player player) {
            Position = position;
            _target = player;
            Damage = 10;
        }

        protected override float _getRadius() {
            return 15;
        }

        protected override float _getAttackRadius() {
            return 140;
        }

        protected override Texture2D _getTexture() {
            return TextureManager.getRangedEnemyTexture();
        }

        protected override Color getColor() {
            return Color.BurlyWood;
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackingRangedTwoWays();
            behaviour.onBehaviourEnded += onAttackBehaviourEnded;
            return behaviour;
        }

        void onAttackBehaviourEnded(Behaviour behaviour) {
            State = EnemyStates.EngagingPlayer;
        }
    }
}

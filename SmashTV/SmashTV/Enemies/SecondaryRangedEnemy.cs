using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {
    public class SecondaryRangedEnemy : EnemyBase {

        public SecondaryRangedEnemy(Vector2 position, Player player) {
            Position = position;
            _target = player;
            Damage = DataDefines.ID_RANGE2_DAMAGE;
        }

        protected override float _getRadius() {
            return DataDefines.ID_RANGE2_RADIUS;
        }

        protected override float _getAttackRadius() {
            return DataDefines.ID_RANGE2_ATTACK_RADIUS;
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getRangedEnemyTexture();
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

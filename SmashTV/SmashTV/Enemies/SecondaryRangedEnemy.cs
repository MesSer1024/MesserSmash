using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {
    public class SecondaryRangedEnemy : EnemyBase {
        private float _preferredMultiplier;
        private Vector2 _aimRandomness;

        public SecondaryRangedEnemy(Vector2 position, Player player) {
            Position = position;
            _target = player;
            Damage = DataDefines.ID_RANGE2_DAMAGE;
            float variationSize = 0.45f;
            _preferredMultiplier = (float)(Utils.random() * variationSize + (1-variationSize/2));
            float maxAimOffset = 115.0f * Utils.getResolutionScale();
            _aimRandomness = new Vector2((float)(Utils.random() * maxAimOffset) - maxAimOffset/2);
        }

        protected override float _getRadius() {
            return DataDefines.ID_RANGE2_RADIUS * Utils.getResolutionScale();
        }

        protected override float _getAttackRadius() {
            return DataDefines.ID_RANGE2_ATTACK_RADIUS * Utils.getResolutionScale();
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getRangedEnemyTexture();
        }

        protected override Color getColor() {
            return Color.BurlyWood;
        }

        protected override float _getScore() {
            return DataDefines.ID_RANGE2_SCORE;
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackingRangeBehaviour(DataDefines.ID_RANGE2_PREFERRED_DISTANCE_FROM_PLAYER * _preferredMultiplier * Utils.getResolutionScale(), _aimRandomness);
            behaviour.onBehaviourEnded += onAttackBehaviourEnded;
            return behaviour;
        }

        void onAttackBehaviourEnded(Behaviour behaviour) {
            behaviour.onBehaviourEnded -= onAttackBehaviourEnded;
            State = EnemyStates.EngagingPlayer;
        }
    }
}

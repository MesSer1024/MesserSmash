using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash.Behaviours {
    using Microsoft.Xna.Framework;
    using MesserSmash.Enemies;
    using MesserSmash.Modules;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AttackingMeleeBehaviour : Behaviour {      
        private const float TIME_ATTACK_ANIM_START = 0.155f;
        private const float TIME_ATTACK_ANIM_END = 0.55f;

        private readonly float _movementSpeed;
        private bool _reachedPlayerInAttackAnimation;
        private readonly Vector2 _fallbackPosition;

        public AttackingMeleeBehaviour(Vector2 fallbackPosition) {
            _movementSpeed = 250;
            _fallbackPosition = fallbackPosition;
        }

        protected override void onUpdate() {
            if (TimeThisBehaviour <= TIME_ATTACK_ANIM_START) {
                Vector2 dir = Utils.safeNormalize(Target.Position - Position);
                Velocity = dir * _movementSpeed * DeltaTime;
                Position += Velocity;
            } else if (TimeThisBehaviour >= TIME_ATTACK_ANIM_START && TimeThisBehaviour <= TIME_ATTACK_ANIM_END) {
                if (!_reachedPlayerInAttackAnimation && PathFinder.overlaps(Enemy, Target)) {
                    _reachedPlayerInAttackAnimation = true;
                    notifyReachedPlayer();
                }
                Vector2 dir = Utils.safeNormalize(_fallbackPosition - Position);
                Velocity = dir * _movementSpeed * DeltaTime;
                Position += Velocity;

            } else {
                //finished with attack animation
                Position = _fallbackPosition;
                notifyDone();
            }  
        }

        private void notifyReachedPlayer() {
            new AttackPlayerCommand(Enemy, Target).execute();
        }
    }
}

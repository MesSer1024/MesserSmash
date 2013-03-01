using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash.Behaviours {
    using Microsoft.Xna.Framework;
    using MesserSmash.Enemies;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AttackingMeleeBehaviour : Behaviour {        
        private readonly float _movementSpeed;
        private bool _reachedPlayerInAttackAnimation;
        private readonly Vector2 _fallbackPosition;

        public AttackingMeleeBehaviour(Vector2 fallbackPosition) {
            _movementSpeed = 200;
            _fallbackPosition = fallbackPosition;
        }

        protected override void onUpdate() {
            if (TimeActive <= 0.25f) {
                Vector2 dir = Utils.safeNormalize(Target.Position - Position);
                Velocity = dir * _movementSpeed * DeltaTime;
                Position += Velocity;
            } else if (TimeActive >= 0.25f && TimeActive <= 0.75f) {
                if (_reachedPlayerInAttackAnimation == false) {
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

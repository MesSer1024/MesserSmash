// -----------------------------------------------------------------------
// <copyright file="AttackingRangedTwoWays.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;

namespace MesserSmash.Behaviours {

    public class AttackingRangedTwoWays : AttackingRangeBehaviour{
        private readonly bool _rotateLeft;

        public AttackingRangedTwoWays() {
            _rotateLeft = Utils.randomBool();
        }

        protected override void updatePlayerPosition() {
            if (shouldMoveTowardsPlayer()) {
                var difference = Target.Position - Position;
                var dir = Utils.safeNormalize(difference);
                Velocity = dir * ENEMY_MOVEMENT_SPEED * DeltaTime;
                Position += Velocity;
            } else {
                var difference = Target.Position - Position;
                var rotation = _rotateLeft ? rotate270Degrees(difference) : rotate90Degrees(difference);
                var dir = Utils.safeNormalize(rotation);
                Velocity = dir * ENEMY_MOVEMENT_SPEED * DeltaTime;
                new BoundaryChecker().restrictMovementToBounds(this);
                Position += Velocity;
            }
        }

        private Vector2 rotate270Degrees(Vector2 v) {
        // (A,B) -> 90 : (-B, A) -> 180: (-A,-B) -> 270: (B, -A)
            float temp = v.X;
            v.X = v.Y;
            v.Y = temp*-1;
            return v;
        }
    }
}

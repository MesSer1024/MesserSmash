using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MesserSmash.Behaviours {
using Microsoft.Xna.Framework;
using MesserSmash.Enemies;
    public class EngagingMeleeUnitBehaviour : Behaviour{
        private readonly float _movementSpeed;

        public EngagingMeleeUnitBehaviour() {
            _movementSpeed = 50;
        }

        protected override void onUpdate() {
            Vector2 dir = Utils.safeNormalize(Target.Position - Position);
            Velocity = dir * _movementSpeed * DeltaTime;
            Position += Velocity;
        }
    }
}

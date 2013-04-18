// -----------------------------------------------------------------------
// <copyright file="AttackWithCharge.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;

namespace MesserSmash.Behaviours {

    public class AttackWithCharge : Behaviour {
        private float _chargeSpeed;
        private readonly float MAX_CHARGE_TIME;
        private float _movementSpeed;
        private float _chargeTimer;

        public AttackWithCharge(float moveSpeed = 50, float chargeSpeed = 260, float chargeTime = 0.86f) {
            _movementSpeed = moveSpeed;
            _chargeSpeed = chargeSpeed;
            MAX_CHARGE_TIME = chargeTime;
            _chargeTimer = 0;
            PathFindEnabled = false;
        }

        protected override void onUpdate() {
            update();
        }

        private void update() {
            var distance = Target.Position - Position;
            var lengthBetweenEntities = distance.Length() - Enemy.Radius - Target.Radius;
            bool reachedPlayer = false;
            bool finished = false;
            _chargeTimer += DeltaTime;

            if (_chargeTimer <= MAX_CHARGE_TIME) {
                if (lengthBetweenEntities < _chargeSpeed * DeltaTime) {
                    reachedPlayer = true;
                    finished = true;
                    Velocity = Utils.safeNormalize(distance) * lengthBetweenEntities;
                } else {
                    Vector2 dir = Utils.safeNormalize(Target.Position - Position);
                    Velocity = dir*_chargeSpeed*DeltaTime;
                }
            } else {
                Velocity = Utils.safeNormalize(distance)*_movementSpeed*DeltaTime;
                finished = true;
            }
            Position += Velocity;

            if (reachedPlayer) {
                notifyReachedPlayer();
            }
            if (finished) {
                notifyDone();
            }
        }

        private void notifyReachedPlayer() {
            new AttackPlayerCommand(Enemy, Target).execute();
        }
    }
}

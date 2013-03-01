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
        private int _chargeSpeed;
        private const float ATTACK_ANIMATION_CD = 5 - MAX_CHARGE_TIME;
        private float _chargeTimer;
        private bool _charging;
        private const float MAX_CHARGE_TIME = 0.66f;
        private float _movementSpeed;

        public AttackWithCharge() {
            _movementSpeed = 50;
            _chargeSpeed = 260;
            _charging = true;
            CollisionEnabled = false;
        }

        protected override void onUpdate() {
            if (_charging) {
                updateCharging();
            } else {
                updateWithoutCharge();
            }
        }

        private void updateCharging() {
            var distance = Target.Position - Position;
            var lengthBetweenEntities = distance.Length() - Enemy.Radius - Target.Radius;
            bool reachedPlayer = false;
            _chargeTimer += DeltaTime;

            if (_chargeTimer <= MAX_CHARGE_TIME) {
                if (lengthBetweenEntities < _chargeSpeed * DeltaTime) {
                    reachedPlayer = true;
                    _chargeTimer = 0;
                    _charging = false;
                    Velocity = Utils.safeNormalize(distance) * lengthBetweenEntities;
                } else {
                    Vector2 dir = Utils.safeNormalize(Target.Position - Position);
                    Velocity = dir*_chargeSpeed*DeltaTime;
                }
            } else {
                _charging = false;
                _chargeTimer = 0;
                Velocity = Utils.safeNormalize(distance)*_movementSpeed*DeltaTime;
            }
            Position += Velocity;

            if (reachedPlayer) {
                _charging = false;
                notifyReachedPlayer();
            }
        }

        private void updateWithoutCharge() {
            _chargeTimer += DeltaTime;
            Vector2 dir = Utils.safeNormalize(Target.Position - Position);
            Velocity = dir * _movementSpeed * DeltaTime;
            Position += Velocity;

            if (canChangeBehavior()) {
                notifyDone();
            }
        }

        private void notifyReachedPlayer() {
            new AttackPlayerCommand(Enemy, Target).execute();
        }

        private bool canChangeBehavior() {
            //TODO put this cooldown in the Enemy class isntead?
            //the idea behind this is to disable next switch between engage->attacking that would automatically occur if player exists inside attack range and is done. 
            //The end result would be that the enemy would constnantly attack the player if he exists inside attack range
            return _chargeTimer > ATTACK_ANIMATION_CD;
        }

    }
}

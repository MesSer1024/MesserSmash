using Microsoft.Xna.Framework;
using MesserSmash.Weapons;
using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;

namespace MesserSmash.Behaviours {

    public class AttackingRangeBehaviour : Behaviour, IEntity {
        private float _noOfShotsFired;
        private const float TIME_BETWEEN_SHOTS = 0.0475f;
        private const int CHANCE_FIRE_SHOT = 31;
        private const float TIME_BETWEEN_BEHAVIOR_SHIFTS = 1.25f;
        private const float MAX_SHOTS_RANGED_ENEMY = 3;
        protected const float ENEMY_MOVEMENT_SPEED = 50;
        private float _timeSinceLastShot;
        private readonly bool _rotateLeft;
        private float _preferredDistance;
        private Vector2 _aimRandomness;
        public UInt16 Identifier { get; private set; }

        public AttackingRangeBehaviour(float preferredDistanceToPlayer, Vector2 aimRandomness) {
            Identifier = Utils.NextIdentifier;

            _aimRandomness = aimRandomness;
            _preferredDistance = preferredDistanceToPlayer;
            _timeSinceLastShot = 0 - TIME_BETWEEN_BEHAVIOR_SHIFTS + TIME_BETWEEN_SHOTS;
            _rotateLeft = Utils.randomBool();
        }

        protected override void onUpdate() {
            _timeSinceLastShot += DeltaTime;
            if (canFireGun() && randomHappening()) {
                createShot();
            } else {
                updatePlayerPosition();
            }

            if (hasFiredAllShots() && canChangeBehavior()) {
                notifyDone();
            }
        }

        private bool randomHappening() {
            return Utils.randomHappening(CHANCE_FIRE_SHOT * DeltaTime);
        }

        private bool canFireGun() {
            return shotCooldown() && !hasFiredAllShots();
        }

        private bool shotCooldown() {
            return _timeSinceLastShot >= TIME_BETWEEN_SHOTS;
        }

        private bool hasFiredAllShots() {
            return _noOfShotsFired >= MAX_SHOTS_RANGED_ENEMY;
        }

        private void createShot() {
            _timeSinceLastShot = 0;
            _noOfShotsFired++;
            new EnemyPistolShot(Position, Target.Position - Position + _aimRandomness);
        }

        protected void updatePlayerPosition() {
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
            v.Y = temp * -1;
            return v;
        }

        protected bool shouldMoveTowardsPlayer() {
            var difference = Target.Position - Position;
            return (difference.Length() > _preferredDistance);
        }

        protected Vector2 rotate90Degrees(Vector2 v) {
            // (A,B) -> (-B, A) after 90 degree rotation
            float temp = v.X;
            v.X = v.Y*-1;
            v.Y = temp;
            return v;
        }

        private bool canChangeBehavior() {
            //TODO put this cooldown in the enemies weapon instead
            //the idea behind this is to disable next switch between engage->attacking that would automatically occur if player exists inside attack range and is done. 
            //The end result would be that the player would constnantly attack the player if he exists inside attack range
            return _timeSinceLastShot > TIME_BETWEEN_BEHAVIOR_SHIFTS;
        }


        public float Radius {
            get { return Enemy.Radius; }
        }

        public bool IsAlive {
            get { return false; }
        }


        public void die(bool runDeathLogic) {
            throw new System.NotImplementedException();
        }
    }
}

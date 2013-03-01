using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MesserSmash {
    class SprintState : PlayerState{
        private const float SPRINT_MOVEMENT_SPEED = 90 * 2.45f;

        public SprintState(Player player) : base(player) { }

        public override void enter() {
        }

        public override float getMovementSpeed() {
            return SPRINT_MOVEMENT_SPEED;
        }

        public override void validate(float deltatime) {
            if (EnergySystem.Instance.canRun(deltatime) == false) {
                exit();
            }
        }

        public override void exit() {
            _player.tryChangeState(new NormalState(_player));
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
            throw new NotImplementedException();
        }

        public override void handleInput() {
            checkForShots();
            if (Utils.isKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftControl)) {
                exit();
            }
        }
    }
}

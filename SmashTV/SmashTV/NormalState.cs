using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash {
    class NormalState : PlayerState {
        private const float MOVEMENT_SPEED = 135;

        public NormalState(Player player) : base(player) { }

        public override void enter() {

        }

        public override void validate(float deltatime) {

        }

        public override float getMovementSpeed() {
            return MOVEMENT_SPEED;
        }

        public override void exit() {

        }
        public override void draw(SpriteBatch sb) {

        }

        public override void handleInput() {
            checkForShots();
            if (Utils.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl)) {
                if (EnergySystem.Instance.canActivateSprintMode())
                    _player.tryChangeState(new SprintState(_player));
            }            
        }
    }
}

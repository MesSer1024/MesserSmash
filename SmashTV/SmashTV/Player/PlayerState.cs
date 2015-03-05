using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MesserSmash {
    public class PlayerState {
        protected Player _player;

        public PlayerState(Player player) {
            _player = player;
        }

        public virtual void enter() {}
        public virtual void validate(float deltatime) {}
        public virtual float getMovementSpeed() {return 0;}
        public virtual void exit() {}
        public virtual void draw(SpriteBatch sb) {}
        public virtual void handleInput() {}

        public virtual void checkForShots() {
            if (Utils.LmbPressed) {
                _player.tryFireLMB();
            }
            if (Utils.RmbPressed) {
                _player.tryFireRMB();
            }
        }

        //protected float 
    }
}

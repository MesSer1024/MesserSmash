using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash.GUI {
    interface IScreen {
        void initialize();
        void update(float deltatime);
        void draw(SpriteBatch sb);
        void destroy();
    }
}

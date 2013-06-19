using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MesserSmash.Enemies {
    public interface IEntity {
        UInt16 Identifier { get; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        float Radius { get; }
        bool IsAlive { get; }
        bool CollisionEnabled { get; }
        void die(bool runDeathLogic);
    }
}

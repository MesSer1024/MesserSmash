using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Enemies;

namespace MesserSmash.Modules {
    public class BoundaryChecker {
        private bool _somethingChanged;

        public void restrictMovementToBounds(IEntity entity) {
            //set velocity so the unit can't move outside of bounds
            Vector2 delta = entity.Position + entity.Velocity;
            Rectangle bounds = SmashTVSystem.Instance.Arena.Bounds;
            if (delta.X + entity.Radius >= bounds.Right) {
                var x = bounds.Right - entity.Radius - entity.Position.X;
                entity.Velocity = new Vector2(x, entity.Velocity.Y);
                _somethingChanged = true;
            } else if (delta.X - entity.Radius <= bounds.Left) {
                var x = bounds.Left + entity.Radius - entity.Position.X;
                entity.Velocity = new Vector2(x, entity.Velocity.Y);
                _somethingChanged = true;
            }
            if (delta.Y + entity.Radius >= bounds.Bottom) {
                var Y = bounds.Bottom - entity.Radius - entity.Position.Y;
                entity.Velocity = new Vector2(entity.Velocity.X, Y);
                _somethingChanged = true;
            } else if (delta.Y - entity.Radius <= bounds.Top) {
                var Y = bounds.Top + entity.Radius - entity.Position.Y;
                entity.Velocity = new Vector2(entity.Velocity.X, Y);
                _somethingChanged = true;
            }
        }

        public bool entityWasOutsideBoundsBeforeRestriction(IEntity unit) {
            _somethingChanged = false;
            restrictMovementToBounds(unit);
            return _somethingChanged;
        }

        public void moveToWall(IEntity entity, Vector2 direction) {
            Rectangle bounds = SmashTVSystem.Instance.Arena.Bounds;
            while (isInsideBounds(entity.Position + direction, bounds)) {
                entity.Position += direction;
            }
            int bajs=0;
        }

        private bool isInsideBounds(Vector2 pos, Rectangle bounds) {
            return bounds.Intersects(new Rectangle((int)pos.X, (int)(pos.Y), 1, 1));
        }
    }
}

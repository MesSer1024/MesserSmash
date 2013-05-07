using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SharedSmashResources {
    public class MesserKeys {
        public Keys[] PressedKeys { get; set; }

        public MesserKeys() {
            PressedKeys = new Keys[0];
        }

        public static MesserKeys Create(KeyboardState state) {
            var mouse = new MesserKeys {
                PressedKeys = state.GetPressedKeys()
            };
            return mouse;
        }

        public bool IsKeyDown(Keys key) {
            return PressedKeys.Contains(key);
        }

        public bool IsKeyUp(Keys key) {
            return PressedKeys.Contains(key) == false;
        }
    }
}

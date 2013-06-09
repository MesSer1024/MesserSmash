using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SharedSmashResources {
    public class MesserKeys {
        public Keys[] PressedKeys { get; set; }

        public MesserKeys() {
            PressedKeys = new Keys[] {};
        }

        public static MesserKeys Create(KeyboardState state) {
            var keyb = new MesserKeys {
                PressedKeys = state.GetPressedKeys()
            };
            return keyb;
        }

        public bool IsKeyDown(Keys key) {
            return PressedKeys.Contains(key);
        }

        public bool IsKeyUp(Keys key) {
            return PressedKeys.Contains(key) == false;
        }
    }
}

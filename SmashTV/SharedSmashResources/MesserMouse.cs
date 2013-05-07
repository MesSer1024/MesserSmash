using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SharedSmashResources {
    public class MesserMouse {
        public bool LeftButton { get; set; }
        public bool MiddleButton { get; set; }
        public bool RightButton { get; set; }
        public int ScrollWheelValue { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool XButton1 { get; set; }
        public bool XButton2 { get; set; }

        public static MesserMouse Create(MouseState state) {
            var mouse = new MesserMouse {
                LeftButton = state.LeftButton == ButtonState.Pressed,
                MiddleButton = state.MiddleButton == ButtonState.Pressed,
                RightButton = state.RightButton == ButtonState.Pressed,
                ScrollWheelValue = state.ScrollWheelValue,
                X = state.X,
                XButton1 = state.XButton1 == ButtonState.Pressed,
                XButton2 = state.XButton2 == ButtonState.Pressed,
                Y = state.Y
            };
            return mouse;
        }
    }
}

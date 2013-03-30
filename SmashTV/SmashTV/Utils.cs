using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MesserSmash.Arenas;
using MesserSmash.Modules;

namespace MesserSmash {
    static class Utils {
        private static KeyboardState _oldState;
        private static KeyboardState _newState = new KeyboardState();
        private static Random rng = new Random();
        private static Vector2 _mousePos;
        private static Vector2 _screenSize;
        private static MouseState _oldMouseState;
        private static MouseState _newMouseState = new MouseState();

        public static void tickInputStates() {
            _oldState = _newState;
            _oldMouseState = _newMouseState;

            _newState = Keyboard.GetState();
            _newMouseState = Mouse.GetState();
            _mousePos = new Vector2(_newMouseState.X, _newMouseState.Y);
        }

        public static bool isNewKeyPress(Keys key) {
            if (_newState.IsKeyDown(key) && _oldState.IsKeyUp(key)) {
                return true;
            }
            return false;
        }

        public static string makeString(string str, params object[] args) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(str, args);
            return sb.ToString();
        }

        public static List<Arena.LootType> generateLootTable(int noOfEnemies, int noOfMoneyBags, int noOfHealthPacks) {
            List<Arena.LootType> myList = new List<Arena.LootType>(noOfEnemies);
            for (int i = 0; i < noOfEnemies; ++i) {
                myList.Add(Arena.LootType.Empty);
            }

            int totalCreations = noOfMoneyBags + noOfHealthPacks;
            while (totalCreations > 0) {
                int item = rng.Next(noOfEnemies);
                if (myList[item] == Arena.LootType.Empty) {
                    --totalCreations;
                    if (noOfHealthPacks > 0) {
                        --noOfHealthPacks;
                        myList[item] = Arena.LootType.Health;
                    } else {
                        --noOfMoneyBags;
                        myList[item] = Arena.LootType.Money;
                    }
                }
            }


            return myList;
        }

        public static string secondsToMMSS(int seconds) {
            int minutes = seconds / 60;
            seconds = seconds % 60;
            return makeString("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
        }

        public static bool isKeyDown(Keys key) {
            return _newState.IsKeyDown(key);
        }

        public static bool isKeyUp(Keys key) {
            return _newState.IsKeyUp(key);
        }

        public static Keys[] getPressedKeys() {
            return _newState.GetPressedKeys();
        }

        public static Vector2 safeNormalize(Vector2 vector) {
            Vector2 value = vector;
            if (value.LengthSquared() == 0) {
                return value;
            }
            value.Normalize();
            return value;
        }

        public static Vector2 getMousePos() {
            return _mousePos;
        }

        public static void setGameSize(int width, int height) {
            _screenSize = new Vector2(width, height);
        }

        public static int getGameWidth() {
            return (int)_screenSize.X;
        }

        public static int getGameHeight() {
            return (int)_screenSize.Y;
        }

        public static double random() {
            return rng.NextDouble();
        }

        public static bool randomBool() {
            if (rng.NextDouble() > 0.5) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for returning integers ranging from 0 to EXCLUSIVE max, so randomInt(100) would give [0..99]
        /// </summary>
        /// <param name="max">EXCLUSIVE max value</param>
        /// <returns></returns>
        public static int randomInt(int max) {
            return rng.Next(max);
        }

        public static Vector2 randomPositionWithinRectangle(Rectangle bounds) {
            Vector2 point = Vector2.Zero;
            point.X = rng.Next(bounds.Left, bounds.Right + 1);
            point.Y = rng.Next(bounds.Top, bounds.Bottom + 1);
            return point;
        }

        public static void rotateVector(ref Vector2 pos, float angle) {
            //pos.X = pos.X * 
            //x = (x * cos(90)) - (y * sin(90))
            //y =(y * cos(90)) + (x * sin(90))

            var cosAngle = Math.Cos(angle);
            var sinAngle = Math.Sin(angle);
            var dx = pos.X * cosAngle - pos.Y * sinAngle;
            var dy = pos.X * sinAngle - pos.Y * cosAngle;
            pos.X = (float)dx;
            pos.Y = (float)dy;
        }

        public static bool valueBetween(object value, object lower, object upper)
        {
            //try {
                var v = Convert.ToDouble(value);
                var l = Convert.ToDouble(lower);
                var u = Convert.ToDouble(upper);

                return v >= l && v <= u;
            //} catch (Exception e) {
            //    Logger.error("Error parsing values as doubles -- {0} {1} {2}", value, lower, upper);
            //    return false;
            //}
        }

        public static bool anyEquals(object value, params object[] args) {
            for (int i = 0; i < args.Length; i++) {
                if (value == args[i])
                    return true;
            }
            return false;
        }
    }
}

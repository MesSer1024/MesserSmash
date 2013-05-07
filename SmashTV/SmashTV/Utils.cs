using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MesserSmash.Arenas;
using MesserSmash.Modules;
using SharedSmashResources;

namespace MesserSmash {
    static class Utils {
        public static int Seed { get; private set; }

        private static MesserKeys _oldState;
        private static MesserKeys _newState;
        private static Random rng;
        private static Vector2 _mousePos;
        private static Vector2 _screenSize;
        private static MesserMouse _oldMouseState;
        private static MesserMouse _newMouseState;

        public static void initialize(int seed) {
            Seed = seed;
            rng = new Random(Seed);
            _newState = new MesserKeys();
            _newMouseState = new MesserMouse();
        }

        public static void tick() {            
            _oldState = _newState;
            _oldMouseState = _newMouseState;

            _newState = MesserKeys.Create(Keyboard.GetState());
            _newMouseState = MesserMouse.Create(Mouse.GetState());
            _mousePos = new Vector2(_newMouseState.X, _newMouseState.Y);
        }

        public static bool LmbPressed { get { return _newMouseState.LeftButton; } }
        public static bool RmbPressed { get { return _newMouseState.RightButton; } }

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
            return _newState.PressedKeys;
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

        /// <summary>
        /// Find out whether something with a certain percentage (0..100) should happen or not
        /// </summary>
        /// <param name="percentage">a value [0..99] to see if it would happen or not (basically value >= random(100))</param>
        /// <returns></returns>
        public static bool randomHappening(float percentage) {
            double rnd = random();
            return rnd * 100 < percentage;
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

        internal static List<Spawnpoint> generateSpawnpoints(Rectangle bounds) {
            var list = new List<Spawnpoint>();
            list.Add(new Spawnpoint(new Rectangle(bounds.Center.X - 140, bounds.Bottom - 60, 60, 60),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Center.X + 100, bounds.Top, 60, 60),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Left, bounds.Center.Y, 60, 60),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Right - 60, bounds.Center.Y, 60, 60),
                                    AssetManager.getArenaTexture()));
            return list;
        }

        internal static bool isEitherNewlyPressed(params Keys[] keys) {
            for (int i = 0; i < keys.Length; i++) {
                var key = keys[i];
                if (_newState.IsKeyDown(key) && _oldState.IsKeyUp(key)) {
                    return true;
                }
            }
            return false;
        }

        internal static MesserMouse getMouseState() {
            return MesserMouse.Create(Mouse.GetState());
        }

        internal static MesserKeys getKeyboardState() {
            return MesserKeys.Create(Keyboard.GetState());
        }

        internal static void forceState(StatusUpdate loadedGame, int replayIndex) {
            _newMouseState = loadedGame.MouseStates[replayIndex];
            _oldMouseState = loadedGame.MouseStates[Math.Max(0, replayIndex-1)];
            _newState = loadedGame.KeyboardStates[replayIndex];
            _oldState = loadedGame.KeyboardStates[Math.Max(0, replayIndex - 1)];
            _mousePos = new Vector2(_newMouseState.X, _newMouseState.Y);
        }
    }
}

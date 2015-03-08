using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MesserSmash.Arenas;
using MesserSmash.Modules;
using SharedSmashResources;
using Microsoft.Xna.Framework.GamerServices;

namespace MesserSmash {
    static class Utils {
        public static int Seed { get; private set; }

        private static MesserKeys _oldKeyboardState;
        private static MesserKeys _keyboardState;
        private static Vector2 _mousePos;
        private static Vector2 _screenSize;
        private static MesserMouse _oldMouseState;
        private static MesserMouse _mouseState;
        private static MesserKeys _nonForcedKeyboardState;
        private static MesserKeys _nonForcedOldKeyboardState;
        private static UInt16 Identifier = UInt16.MinValue;
        public static UInt16 NextIdentifier { get { return ++Identifier; } }

        public static class NonForcedKeyboard {
            public static bool isNewKeyPress(Keys key) {
                if (_nonForcedKeyboardState.IsKeyDown(key) && _nonForcedOldKeyboardState != null && _nonForcedOldKeyboardState.IsKeyUp(key)) {
                    return true;
                }
                return false;
            }

            internal static bool isEitherNewlyPressed(params Keys[] keys) {
                for (int i = 0; i < keys.Length; i++) {
                    var key = keys[i];
                    if (_nonForcedKeyboardState.IsKeyDown(key) && _nonForcedOldKeyboardState != null && _nonForcedOldKeyboardState.IsKeyUp(key)) {
                        return true;
                    }
                }
                return false;
            }

            public static bool isKeyDown(Keys key) {
                return _nonForcedKeyboardState.IsKeyDown(key);
            }

            public static bool isKeyUp(Keys key) {
                return _nonForcedKeyboardState.IsKeyUp(key);
            }

            public static Keys[] getPressedKeys() {
                return _nonForcedKeyboardState.PressedKeys;
            }
        }

        public static void delayCall(Action action, int delayMS = 10) {
            new System.Threading.Timer(obj =>
            {
                action.Invoke();
            },
                null,
                delayMS,
                System.Threading.Timeout.Infinite
                );
        }


        public static void initialize(int seed) {
            Logger.info("Utils.initialize seed=" + seed);
            Seed = seed;
            MesserRandom.init(seed);
            //cant set them here due to it making it possible to hold down F1 and create a basillion games
            //_keyboardState = new MesserKeys();
            //_mouseState = new MesserMouse();

            _nonForcedKeyboardState = new MesserKeys();
            _nonForcedOldKeyboardState = new MesserKeys();
        }

        public static void tick() {            
            _oldKeyboardState = _keyboardState;
            _oldMouseState = _mouseState;

            _keyboardState = MesserKeys.Create(Keyboard.GetState());
            _mouseState = MesserMouse.Create(Mouse.GetState());
            _mousePos = new Vector2(_mouseState.X, _mouseState.Y);
        }

        public static bool LmbPressed { get { return _mouseState.LeftButton; } }
        public static bool RmbPressed { get { return _mouseState.RightButton; } }

        public static bool isNewKeyPress(Keys key) {
            if (_keyboardState.IsKeyDown(key) && _oldKeyboardState != null && _oldKeyboardState.IsKeyUp(key)) {
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
                int item = MesserRandom.nextInt(noOfEnemies);
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
            return _keyboardState.IsKeyDown(key);
        }

        public static bool isKeyUp(Keys key) {
            return _keyboardState.IsKeyUp(key);
        }

        public static Keys[] getPressedKeys() {
            return _keyboardState.PressedKeys;
        }

        public static string keyOutputCharacter(Keys key) {
            char v = '\0';
            TryConvertKeyboardInput(key, false, out v);
            return v.ToString().ToUpper();
        }

        private static bool TryConvertKeyboardInput(Keys keys, bool shift, out char key) {
            switch (keys) {
                //Alphabet keys
                case Keys.A:
                    if (shift) { key = 'A'; } else { key = 'a'; }
                    return true;
                case Keys.B:
                    if (shift) { key = 'B'; } else { key = 'b'; }
                    return true;
                case Keys.C:
                    if (shift) { key = 'C'; } else { key = 'c'; }
                    return true;
                case Keys.D:
                    if (shift) { key = 'D'; } else { key = 'd'; }
                    return true;
                case Keys.E:
                    if (shift) { key = 'E'; } else { key = 'e'; }
                    return true;
                case Keys.F:
                    if (shift) { key = 'F'; } else { key = 'f'; }
                    return true;
                case Keys.G:
                    if (shift) { key = 'G'; } else { key = 'g'; }
                    return true;
                case Keys.H:
                    if (shift) { key = 'H'; } else { key = 'h'; }
                    return true;
                case Keys.I:
                    if (shift) { key = 'I'; } else { key = 'i'; }
                    return true;
                case Keys.J:
                    if (shift) { key = 'J'; } else { key = 'j'; }
                    return true;
                case Keys.K:
                    if (shift) { key = 'K'; } else { key = 'k'; }
                    return true;
                case Keys.L:
                    if (shift) { key = 'L'; } else { key = 'l'; }
                    return true;
                case Keys.M:
                    if (shift) { key = 'M'; } else { key = 'm'; }
                    return true;
                case Keys.N:
                    if (shift) { key = 'N'; } else { key = 'n'; }
                    return true;
                case Keys.O:
                    if (shift) { key = 'O'; } else { key = 'o'; }
                    return true;
                case Keys.P:
                    if (shift) { key = 'P'; } else { key = 'p'; }
                    return true;
                case Keys.Q:
                    if (shift) { key = 'Q'; } else { key = 'q'; }
                    return true;
                case Keys.R:
                    if (shift) { key = 'R'; } else { key = 'r'; }
                    return true;
                case Keys.S:
                    if (shift) { key = 'S'; } else { key = 's'; }
                    return true;
                case Keys.T:
                    if (shift) { key = 'T'; } else { key = 't'; }
                    return true;
                case Keys.U:
                    if (shift) { key = 'U'; } else { key = 'u'; }
                    return true;
                case Keys.V:
                    if (shift) { key = 'V'; } else { key = 'v'; }
                    return true;
                case Keys.W:
                    if (shift) { key = 'W'; } else { key = 'w'; }
                    return true;
                case Keys.X:
                    if (shift) { key = 'X'; } else { key = 'x'; }
                    return true;
                case Keys.Y:
                    if (shift) { key = 'Y'; } else { key = 'y'; }
                    return true;
                case Keys.Z:
                    if (shift) { key = 'Z'; } else { key = 'z'; }
                    return true;

                //Decimal keys
                case Keys.D0:
                    if (shift) { key = ')'; } else { key = '0'; }
                    return true;
                case Keys.D1:
                    if (shift) { key = '!'; } else { key = '1'; }
                    return true;
                case Keys.D2:
                    if (shift) { key = '@'; } else { key = '2'; }
                    return true;
                case Keys.D3:
                    if (shift) { key = '#'; } else { key = '3'; }
                    return true;
                case Keys.D4:
                    if (shift) { key = '$'; } else { key = '4'; }
                    return true;
                case Keys.D5:
                    if (shift) { key = '%'; } else { key = '5'; }
                    return true;
                case Keys.D6:
                    if (shift) { key = '^'; } else { key = '6'; }
                    return true;
                case Keys.D7:
                    if (shift) { key = '&'; } else { key = '7'; }
                    return true;
                case Keys.D8:
                    if (shift) { key = '*'; } else { key = '8'; }
                    return true;
                case Keys.D9:
                    if (shift) { key = '('; } else { key = '9'; }
                    return true;

                //Decimal numpad keys
                case Keys.NumPad0:
                    key = '0';
                    return true;
                case Keys.NumPad1:
                    key = '1';
                    return true;
                case Keys.NumPad2:
                    key = '2';
                    return true;
                case Keys.NumPad3:
                    key = '3';
                    return true;
                case Keys.NumPad4:
                    key = '4';
                    return true;
                case Keys.NumPad5:
                    key = '5';
                    return true;
                case Keys.NumPad6:
                    key = '6';
                    return true;
                case Keys.NumPad7:
                    key = '7';
                    return true;
                case Keys.NumPad8:
                    key = '8';
                    return true;
                case Keys.NumPad9:
                    key = '9';
                    return true;

                //Special keys
                case Keys.OemTilde:
                    if (shift) { key = '~'; } else { key = '`'; }
                    return true;
                case Keys.OemSemicolon:
                    if (shift) { key = ':'; } else { key = ';'; }
                    return true;
                case Keys.OemQuotes:
                    if (shift) { key = '"'; } else { key = '\''; }
                    return true;
                case Keys.OemQuestion:
                    if (shift) { key = '?'; } else { key = '/'; }
                    return true;
                case Keys.OemPlus:
                    if (shift) { key = '+'; } else { key = '='; }
                    return true;
                case Keys.OemPipe:
                    if (shift) { key = '|'; } else { key = '\\'; }
                    return true;
                case Keys.OemPeriod:
                    if (shift) { key = '>'; } else { key = '.'; }
                    return true;
                case Keys.OemOpenBrackets:
                    if (shift) { key = '{'; } else { key = '['; }
                    return true;
                case Keys.OemCloseBrackets:
                    if (shift) { key = '}'; } else { key = ']'; }
                    return true;
                case Keys.OemMinus:
                    if (shift) { key = '_'; } else { key = '-'; }
                    return true;
                case Keys.OemComma:
                    if (shift) { key = '<'; } else { key = ','; }
                    return true;
                case Keys.Space:
                    key = ' ';
                    return true;
            }

            key = (char)0;
            return false;
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
            return MesserRandom.next();
        }

        public static bool randomBool() {
            return MesserRandom.nextBool();
        }

        /// <summary>
        /// Method for returning integers ranging from 0 to EXCLUSIVE max, so randomInt(100) would give [0..99]
        /// </summary>
        /// <param name="max">EXCLUSIVE max value</param>
        /// <returns></returns>
        public static int randomInt(int max) {
            return MesserRandom.nextInt(max);
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
            point.X = MesserRandom.nextInt(bounds.Left, bounds.Right + 1);
            point.Y = MesserRandom.nextInt(bounds.Top, bounds.Bottom + 1);
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
                if (value.Equals(args[i]))
                    return true;
            }
            return false;
        }

        internal static List<Spawnpoint> generateSpawnpoints(Rectangle bounds) {

            var list = new List<Spawnpoint>();
            list.Add(new Spawnpoint(new Rectangle(bounds.Center.X - calcResolutionScaledValue(210), bounds.Bottom - calcResolutionScaledValue(90), calcResolutionScaledValue(90), calcResolutionScaledValue(90)),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Center.X + calcResolutionScaledValue(150), bounds.Top, calcResolutionScaledValue(90), calcResolutionScaledValue(90)),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Left, bounds.Center.Y, calcResolutionScaledValue(90), calcResolutionScaledValue(90)),
                                    AssetManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(bounds.Right - calcResolutionScaledValue(90), bounds.Center.Y, calcResolutionScaledValue(90), calcResolutionScaledValue(90)),
                                    AssetManager.getArenaTexture()));
            return list;
        }

        internal static bool isEitherNewlyPressed(params Keys[] keys) {
            for (int i = 0; i < keys.Length; i++) {
                var key = keys[i];
                if (_keyboardState.IsKeyDown(key) && _oldKeyboardState != null && _oldKeyboardState.IsKeyUp(key)) {
                    return true;
                }
            }
            return false;
        }

        internal static MesserMouse getMouseState() {
            return _mouseState;
        }

        internal static MesserKeys getKeyboardState() {
            return _keyboardState;
        }

        internal static void forceState(GameStates loadedGame, int replayIndex) {
            _mouseState = loadedGame.MouseStates[replayIndex];
            _oldMouseState = loadedGame.MouseStates[Math.Max(0, replayIndex-1)];
            _keyboardState = loadedGame.KeyboardStates[replayIndex];
            _oldKeyboardState = loadedGame.KeyboardStates[Math.Max(0, replayIndex - 1)];
            _mousePos = new Vector2(_mouseState.X, _mouseState.Y);
            _nonForcedOldKeyboardState = _nonForcedKeyboardState;
            _nonForcedKeyboardState = MesserKeys.Create(Keyboard.GetState());
        }

        internal static void forceReplayInputUpdate() {
            _nonForcedOldKeyboardState = _nonForcedKeyboardState;
            _nonForcedKeyboardState = MesserKeys.Create(Keyboard.GetState());
        }


        public static Rectangle getGameBounds() {
            return new Rectangle(0, 0, getGameWidth(), getGameHeight());
        }

        public static float getResolutionScale() {
            return Utils.getGameWidth() / 1920f;
        }

        public static int calcResolutionScaledValue(int value) {
            return (int)(value * Utils.getResolutionScale());
        }

        public static float clamp(float curr, float min, float max) {
            return Math.Min(max, Math.Max(min, curr));
        }

        public static void showException(Exception e)
        {
            List<string> MBOPTIONS = new List<string>();
            MBOPTIONS.Add("OK");
            string msg = e.ToString();
            Logger.error(msg);
            Guide.BeginShowMessageBox(
                    "Exception during init()", msg.Substring(0, 255), MBOPTIONS, 0,
                    MessageBoxIcon.Error, null, null);
        }
    }
}

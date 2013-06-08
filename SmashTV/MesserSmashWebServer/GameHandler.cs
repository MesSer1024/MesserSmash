using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using System.Diagnostics;
using SharedSmashResources;
using System.Collections;

namespace MesserSmashWebServer {
    static class GameHandler {
        private static Dictionary<string, object> _data;
        private static int _status;

        private static class inline_reader {
            public static bool readNumber(string key, out double value) {
                try {
                    var o = _data[key];
                    value = Convert.ToDouble(o);
                    return true;
                } catch (Exception e) {
                    value = 0;
                    Logger.error("invalid input key={0}, exception={2}", key, value, e.ToString());
                    return false;
                }
            }

            public static bool readObject(string key, out object value) {
                try {
                    value = _data[key];
                    return true;
                } catch (Exception e) {
                    value = null;
                    Logger.error("invalid input key={0}, exception={2}", key, value, e.ToString());
                    return false;
                }
            }

            public static object readString(string key, out string value) {
                try {
                    value = (string)_data[key];
                    return true;
                } catch (Exception e) {
                    value = "";
                    Logger.error("invalid input key={0}, exception={2}", key, value, e.ToString());
                    return false;
                }
            }
        }

        private static class safe_reader {
            public static double readNumber(string key, double defaultValue = 0) {
                try {
                    var o = _data[key];
                    double d = Convert.ToDouble(o);
                    return d;
                } catch (Exception e) {
                    Logger.error("invalid input key={0} default={1} exception={2}", key, defaultValue, e.ToString());
                }
                return defaultValue;
            }

            public static object readObject(string key, object defaultValue = null) {
                try {
                    var o = _data[key];
                    return o;
                } catch (Exception e) {
                    Logger.error("invalid input key={0} value={1} exception={2}", key, defaultValue, e.ToString());
                }
                return defaultValue;
            }

            public static object readString(string key, string defaultValue = "") {
                try {
                    var o = (string)_data[key];
                    return o;
                } catch (Exception e) {
                    Logger.error("invalid input key={0}, value={1}, exception={2}", key, defaultValue, e.ToString());
                }
                return defaultValue;
            }
        }

        private static class reader {
            public static double readNumber(string key) {
                var o = _data[key];
                double d = Convert.ToDouble(o);
                return d;
            }
            public static double readNumber(string key, out double value) {
                var o = _data[key];
                double d = Convert.ToDouble(o);
                value = d;
                return d;
            }

            public static object readObject(string key, out object value) {
                var o = _data[key];
                value = o;
                return o;
            }

            public static object readString(string key, out string value) {
                var o = (string)_data[key];
                value = o;
                return o;
            }

            public static object readArray(string key, out ArrayList value) {
                var o = _data[key];
                value = o as ArrayList;
                return o;
            }
        }

        public static void verifyDataStartGame(String data, out int status) {
            _data = fastJSON.JSON.Instance.Parse(data) as Dictionary<string, object>;
            
            _status = 0;

            try {
                string key, name, id;
                double level;
                object state;
                reader.readString(MesserSmashWeb.LOGIN_SESSION, out key);
                reader.readString(MesserSmashWeb.USER_NAME, out name);
                reader.readString(MesserSmashWeb.USER_ID, out id);
                reader.readNumber(MesserSmashWeb.LEVEL, out level);
                reader.readObject(MesserSmashWeb.PLAYER_STATE, out state);
            } catch (Exception e) {
                Logger.error("unable to parse, original data={0} \n exception={1}", data, e.ToString());
                _status = 1;
            }

            status = _status;            
        }

        public static void handle(String s) {
            
        }

        internal static void verifyDataStatus(string data, out int status) {
            _data = fastJSON.JSON.Instance.Parse(data) as Dictionary<string, object>;
            _status = 0;

            try {
                double level, kills, score;
                string gameState;
                double energy, gametime, timeMultiplier, seed;
                ArrayList keyboardStates, times, mouseStates;

                reader.readNumber(MesserSmashWeb.LEVEL, out level);
                reader.readNumber(MesserSmashWeb.KILLS, out kills);
                reader.readNumber(MesserSmashWeb.SCORE, out score);
                reader.readNumber(MesserSmashWeb.ENERGY, out energy);
                reader.readNumber(MesserSmashWeb.TOTAL_GAME_TIME, out gametime);
                reader.readNumber(MesserSmashWeb.TIME_MULTIPLIER, out timeMultiplier);
                reader.readNumber(MesserSmashWeb.RANDOM_SEED, out seed);
                reader.readString(MesserSmashWeb.GAME_STATE, out gameState);
                reader.readArray(MesserSmashWeb.DELTA_TIME, out times);
                reader.readArray(MesserSmashWeb.KEYBOARD_STATES, out keyboardStates);
                reader.readArray(MesserSmashWeb.MOUSE_STATES, out mouseStates);
            } catch (Exception e) {
                Logger.error("unable to parse, original data={0} \n exception={1}", data, e.ToString());
                _status = 1;
            }
            status = _status;
        }

        public static double readLevel() {
            double value;
            reader.readNumber(MesserSmashWeb.LEVEL, out value);
            return value;
        }

        public static double readKills() {
            double value;
            reader.readNumber(MesserSmashWeb.KILLS, out value);
            return value;
        }

        internal static double readScore() {
            double value;
            reader.readNumber(MesserSmashWeb.SCORE, out value);
            return value;
        }

        internal static string readUserName() {
            string value;
            reader.readString(MesserSmashWeb.USER_NAME, out value);
            return value;

        }
    }
}

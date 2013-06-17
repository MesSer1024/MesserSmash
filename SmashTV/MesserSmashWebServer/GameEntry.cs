using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmashWebServer {
    class GameEntry {
        [Flags]
        public enum GameStatuses {
            Open = 1 << 0,
            Closed = 1 << 1,
            LevelCompleted = 1 << 2,
            Dead = 1 << 3,
            Aborted = 1 << 4,
        }

        public GameEntry() {
            Ticks = DateTime.Now.Ticks;
        }

        public string LoginKey { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string SessionId { get; set; }
        public long Ticks { get; set; }
        public uint Level { get; set; }
        public GameStatuses Status { get; set; }

        public override string ToString() {
            return String.Format("ticks={0}|sessionid={1}|username={2}|userid={3}|gameid={4}|loginkey={5}|level={6}|status={7}", Ticks, SessionId, UserName, UserId, GameId, LoginKey, Level, (uint)Status);
        }

        public static GameEntry FromString(string rawLine) {
            Dictionary<string, string> table = new Dictionary<string, string>();
            var items = rawLine.Split('|');
            foreach (var pair in items) {
                var item = pair.Split('=');
                table.Add(item[0], item[1]);
            }

            var ret = new GameEntry();
            ret.Ticks = ParseLong(table, "ticks");
            ret.SessionId = ParseString(table, "sessionid");
            ret.UserName = ParseString(table, "username");
            ret.UserId = ParseString(table, "userid");
            ret.GameId = ParseString(table, "gameid");
            ret.Level = ParseUint(table, "level");
            ret.LoginKey = ParseString(table, "loginkey");
            ret.Status = (GameStatuses)ParseUint(table, "status");
            return ret;
        }

        private static long ParseLong(Dictionary<string, string> container, string key, long defaultValue = 0) {
            long v = 0;
            if (container.ContainsKey(key)) {
                var foo = container[key];
                if (long.TryParse(foo, out v)) {
                    return v;
                }
            }
            return defaultValue;
        }

        private static string ParseString(Dictionary<string, string> container, string key, string defaultValue = "") {
            if (container.ContainsKey(key)) {
                return container[key];
            }
            return defaultValue;
        }

        private static uint ParseUint(Dictionary<string, string> container, string key, uint defaultValue = 0) {
            uint v = 0;
            if (container.ContainsKey(key)) {
                var foo = container[key];
                if (uint.TryParse(foo, out v)) {
                    return v;
                }
            }
            return defaultValue;
        }
    }
}

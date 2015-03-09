using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace SharedSmashResources {
    public class Highscore {
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string SessionId { get; set; }
        public long Ticks { get; set; }

        public string UserName { get; set; }
        public uint Level { get; set; }
        public uint RoundId { get; set; }
        public uint Kills { get; set; }
        public uint Score { get; set; }
        public string GameVersion { get; set; }
        public string File { get; set; }
        public bool IsLocalHighscore { get; set; }
        public bool IsVerified { get; set; }
        public bool IsMerged { get; set; }

        public Highscore() {
            IsLocalHighscore = true;            
        }

        public override string ToString() {
            return String.Format("ticks={0}|username={1}|level={2}|userid={3}|gameid={4}|kills={5}|score={6}|version={7}|sessionid={8}|file={9}|roundid={10}", Ticks, UserName, Level, UserId, GameId, Kills, Score, GameVersion, SessionId, File, RoundId);
        }

        public static Highscore FromString(string rawLine) {
            Dictionary<string, string> table = new Dictionary<string, string>();
            var items = rawLine.Split('|');
            foreach (var pair in items) {
                var item = pair.Split('=');
                if (item.Length != 2) {
                    Logger.error("Invalid data in Highscore fromString expected 'KeyValuePair' rawLine was:{0}", rawLine);
                    return null;
                }
                table.Add(item[0], item[1]);
            }

            var ret = new Highscore();
            ret.UserName = ParseString(table, "username");
            ret.UserId = ParseString(table, "userid");
            ret.GameId = ParseString(table, "gameid");
            ret.SessionId = ParseString(table, "sessionid");
            ret.Ticks = ParseLong(table, "ticks");
            ret.Level = ParseUint(table, "level");
            ret.Kills = ParseUint(table, "kills");
            ret.Score = ParseUint(table, "score");
            ret.GameVersion = ParseString(table, "version", "0.0004");
            ret.RoundId = ParseUint(table, "roundid");
            ret.File = ParseString(table, "file", String.Format("{0}_save.txt", ret.Ticks));
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
            if(container.ContainsKey(key)) {
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

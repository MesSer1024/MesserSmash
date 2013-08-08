using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SharedSmashResources;
using MesserSmash.Modules;
using System.Diagnostics;

namespace MesserSmash.Commands {
    class LoadConfigFileCommand : Command {
        public const string NAME = "LoadConfigFileCommand";

        public bool HasUsername { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string ServerIp { get; set; }
        public string ReplayPath { get; set;}
        public string GameVersion { get; set; }
        public bool IgnoreVersion { get; set; }
        public float GameWidth { get; set; }
        public bool LoadedFromLauncher { get; set; }

        public LoadConfigFileCommand()
            : base(NAME) {
            HasUsername = false;
            GameWidth = 1920;

            StringBuilder sb = new StringBuilder();
            using (var sr = new StreamReader("./settings.ini")) {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine().Trim();
                    if (line.StartsWith("username")) {
                        var value = readValue(line) ?? "$_foo";
                        if (value.Length > 0 && value != "$_foo") {
                            HasUsername = true;
                            Username = value;
                        }
                    } else if (line.StartsWith("server_ip")) {
                        ServerIp = readValue(line) ?? "http://localhost:8801/";
                    } else if (line.StartsWith("replay_path")) {
                        var replayId = readValue(line);
                        ReplayPath = replayId ?? "last_save.txt";
                    } else if (line.StartsWith("game_version")) {
                        var version = readValue(line);
                        GameVersion = version ?? "";
                    } else if (line.StartsWith("game_width")) {
                        var w = readValue(line);
                        var f = 0f;
                        if (float.TryParse(w, out f)) {
                            GameWidth = f;
                        }
                    }
                }
            }
            Logger.info("EnvironmentVariable launcher={0}", Environment.GetEnvironmentVariable(MesserSmashWeb.ENVIRONMENT_LAUNCHED_FROM_LAUNCHER));

            if (Environment.GetEnvironmentVariable(MesserSmashWeb.ENVIRONMENT_LAUNCHED_FROM_LAUNCHER) == true.ToString()) {
                LoadedFromLauncher = true;
                HasUsername = true;
                Username = Environment.GetEnvironmentVariable(MesserSmashWeb.USER_NAME);
                UserId = Environment.GetEnvironmentVariable(MesserSmashWeb.USER_ID);
                GameVersion = Environment.GetEnvironmentVariable(MesserSmashWeb.GAME_VERSION);
                Logger.info("EnvironmentVariable username={0}", Username);
                Logger.info("EnvironmentVariable userid={0}", UserId);
                Logger.info("EnvironmentVariable game_version={0}", GameVersion);
            }
        }

        private string readValue(string line) {
            var splitter = '|';
            var splits = line.Split(splitter);
            if (splits.Length != 2) {
                throw new Exception(string.Format("Invalid setting in LoadConfig for line {0}", line));
            }
            var item = splits[1].Trim();
            return item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MesserSmash.Commands {
    class LoadConfigFileCommand : Command {
        public const string NAME = "LoadConfigFileCommand";

        public bool HasUsername { get; set; }
        public string Username { get; set; }
        public string ServerIp { get; set; }
        public string ReplayPath { get; set;}

        public LoadConfigFileCommand()
            : base(NAME) {
            HasUsername = false;

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
                    }
                }
            }
        }

        private string readValue(string line) {
            var splitter = '|';
            var item = line.Split(splitter)[1].Trim();
            return item;
        }
    }
}

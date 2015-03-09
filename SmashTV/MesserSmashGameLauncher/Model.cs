using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace MesserSmashGameLauncher {
    public class Model : DependencyObject {
        private const string SETTINGS_FILE = "./external/launcher_settings.ini";

        public static string Password { get; set; }
        public static string UserName { get; set; }
        public static string UserId { get; set; }
        public static string ClientVersion { get; set; }
        public static string Token { get; set; }
        public static bool Online { get; set; }
        public static string ServerIp { get; set; }

        static Model() {
            Instance = new Model();
        }

        public static Model Instance { get; private set; }

        public static void load() {
            var file = SETTINGS_FILE;
            var fi = new FileInfo(file);
            if (!fi.Exists) {
                MessageBox.Show("Could not find settings file...");
            }
            using (var sr = new StreamReader(file)) {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine().Trim();
                    var value = readValue(line);
                    if (line.StartsWith("username")) {
                        Model.UserName = value;
                    } else if (line.StartsWith("password")) {
                        Model.Password = value;
                    } else if (line.StartsWith("server_ip")) {
                        Model.ServerIp = value;
                    } else if (line.StartsWith("game_version")) {
                        Model.ClientVersion = value;
                    }
                }
            }

            if (Model.ServerIp == "" || Model.ServerIp == null) {
                Model.ServerIp = "http://localhost:8801/";
            }
        }

        private static string readValue(string line) {
            var splitter = '|';
            var splits = line.Split(splitter);
            if (splits.Length != 2) {
                throw new Exception(string.Format("Invalid setting in LoadConfig for line {0}", line));
            }
            var item = splits[1].Trim();
            return item;
        }

        internal static void save() {
            var file = SETTINGS_FILE;
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0} | {1}", "game_version", Model.ClientVersion));
            sb.AppendLine(String.Format("{0} | {1}", "server_ip", "http://pawncraft.co.uk:8801/"));
            sb.AppendLine(String.Format("{0} | {1}", "server_ip", "http://localhost:8801/"));
            sb.AppendLine(String.Format("{0} | {1}", "server_ip", Model.ServerIp));
            sb.AppendLine(String.Format("{0} | {1}", "username", Model.UserName));
            sb.AppendLine(String.Format("{0} | {1}", "password", Model.Password));
            using (var sw = new StreamWriter(file, false)) {
                sw.Write(sb.ToString());
                sw.Flush();
            }
        }
    }
}

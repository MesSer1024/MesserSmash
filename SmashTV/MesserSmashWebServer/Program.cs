using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SharedSmashResources;
using MesserSmash.Modules;

namespace MesserSmashWebServer {
    class Program {
        private static HighscoreContainer _highscores;
        private static string _url;
        //http://pawncraft.co.uk:8801/ this is the external url to the messersmash server

        static void Main(string[] args) {
            var highscoreFilePath = "./highscores/highscores.txt";
            _highscores = new HighscoreContainer();
            Logger.init("MesserSmashServer.txt");
            string url = null;
            var file = new FileInfo("../../../../bin/debug/server_settings.ini");
            if (!file.Exists) {
                file = new FileInfo("./server_settings.ini");
            }
            using (var sr = new StreamReader(file.FullName)) {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine().Trim();
                    var splitter = '|';
                    if (line.StartsWith("server_ip")) {
                        url = line.Split(splitter)[1].Trim();
                    }
                }
            }
            _url = url ?? "http://localhost:8801/";

            _highscores.populateHighscores(highscoreFilePath);

            var server = new MesserSmashWebServer(HandleHttpRequest, url);
            server.Run();
            Console.WriteLine("A simple webserver url:{0} \nPress a key to quit.", url);
            Console.ReadKey();
            server.Stop();
            _highscores.outputToFile(highscoreFilePath);
        }

        private void writeHighscoresToFile(string relativeFilePath) {
            _highscores.outputToFile(relativeFilePath);
        }

        public static string HandleHttpRequest(HttpListenerRequest httpRequest) {
            if (httpRequest == null || !httpRequest.HasEntityBody) { return null; }
            var timestamp = DateTime.Now;
            var request = httpRequest.Headers["request"];
            var data = new byte[httpRequest.ContentLength64];
            try{
                using (System.IO.Stream body = httpRequest.InputStream) {
                    body.Read(data, 0, data.Length);
                };
            }
            catch(Exception e) {
                Console.WriteLine("Unable to parse data from client {0}", e.ToString());
                Logger.error("Unable to parse data from client {0}", e.ToString());            
            }
            switch (request) {
                case SmashWebIdentifiers.REQUEST_SAVE_GAME: {
                        var server = new LocalServer(_url);
                        GameStates states = server.buildGameState(data);
                        if (states != null) {
                            saveGame(states);
                        } else {
                            return string.Format("Foobar | error={0}", states);
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{1}->Handled request in: {0}ms from user={2}[{3}]", (DateTime.Now - timestamp).TotalMilliseconds, DateTime.Now.ToString("dd/MM[HH:mm:ss]"), states.UserName, states.UserId);
                        Console.WriteLine(sb.ToString());
                        Logger.info(sb.ToString());
                        return "OK";
                    }
                case SmashWebIdentifiers.REQUEST_GET_HIGHSCORE_ON_LEVEL: {
                        var server = new LocalServer(_url);
                        try {

                            var rawData = server.readData(data);
                            var foo2 = rawData.Split(';');

                            Dictionary<string, string> items = new Dictionary<string, string>();
                            foreach (var item in foo2) {
                                var a = item.Split('=');
                                if (a.Length != 2)
                                    throw new Exception("REQUEST_GET_HIGHSCORE_ON_LEVEL-data wasn't a key-value pair!");
                                items.Add(a[0], a[1]);
                            }
                            uint level = uint.Parse(items["Level"]);                            
                            var scores = _highscores.getHighscoresOnLevel(level).OrderByDescending(a => a.Score);
                            var sb = new StringBuilder();
                            foreach (var item in scores)
                            {
                                sb.AppendLine(item.ToString());
                            }
                            Console.WriteLine("{1}->Handled getHighscoreRequest in: {0}ms on level:{2}", (DateTime.Now - timestamp).TotalMilliseconds, DateTime.Now.ToString("dd/MM[HH:mm:ss]"), level);
                            Logger.info("{1}->Handled getHighscoreRequest in: {0}ms on level:{2}", (DateTime.Now - timestamp).TotalMilliseconds, DateTime.Now.ToString("dd/MM[HH:mm:ss]"), level);
                            return sb.ToString();
                        } catch (Exception e) {
                            Console.WriteLine("Unable to parse data from client {0}", e.ToString());
                            Logger.error("Unable to parse data from client {0}", e.ToString());
                        }

                    }
                    break;
                default:
                    Logger.error("Unhandled request: {0}", request);
                    break;
            }
            return "status=666";
        }

        private static void saveGame(GameStates data) {
            var folder = new DirectoryInfo(System.Environment.CurrentDirectory);
            folder = folder.CreateSubdirectory("games");

            var timestamp = DateTime.Now.Ticks;
            var outputFile = folder.FullName + "/" + timestamp + "_save.txt";
            using (var sw = new StreamWriter(outputFile)) {
                sw.Write(data.toJson());
                sw.Flush();
            }

            using (var sw = new StreamWriter(folder.FullName + "/last_save.txt", false)) {
                sw.Write(data.toJson());
                sw.Flush();
            }
            _highscores.addHighscore(new Highscore { Ticks = timestamp, File = outputFile, GameId = data.GameId, GameVersion = data.GameVersion, Kills = (uint)data.Kills, Level = (uint)data.Level, Score = (uint)data.Score, SessionId = data.SessionId, UserId = data.UserId, UserName = data.UserName });
        }
    }
}

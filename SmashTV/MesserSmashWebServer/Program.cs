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
        private static LocalServer _server;
        private static ServerDb _gameEntries;
        //http://pawncraft.co.uk:8801/ this is the external url to the messersmash server

        static void Main(string[] args) {
            var highscoreFilePath = "./highscores/highscores.txt";
            _highscores = new HighscoreContainer();
            _gameEntries = new ServerDb();
            Logger.init("MesserSmashServer.txt");

            //MesserSmashTest.test(null);


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

            _highscores.populateHighscoresFromFile(highscoreFilePath);
            _gameEntries.init();

            var server = new MesserSmashWebServer(HandleHttpRequest, _url);
            _server = server.LocalServer;
            server.Run();
            Console.WriteLine("A simple webserver url:{0} \nPress a key to quit.", _url);
            Console.ReadKey();
            server.Stop();
            _highscores.writeHighscoresToFile(highscoreFilePath);
            _gameEntries.close();
        }

        public static string HandleHttpRequest(string request, string rawData) {
            switch (request) {
                case MesserSmashWeb.REQUEST_BEGIN_GAME: {
                    var items = toTable(rawData);
                        var userid = items[MesserSmashWeb.USER_ID].ToString();
                        var username = items[MesserSmashWeb.USER_NAME].ToString();
                        var loginKey = items[MesserSmashWeb.LOGIN_SESSION].ToString();
                        var level = uint.Parse(items[MesserSmashWeb.LEVEL].ToString());

                        var gameid = Guid.NewGuid().ToString();
                        var sessionid = Guid.NewGuid().ToString();

                        var result = new Dictionary<string, object> {
                            {MesserSmashWeb.GAME_ID, gameid},
                            {MesserSmashWeb.SESSION_ID, sessionid}
                        };

                        _gameEntries.addEntry(new GameEntry { UserId = userid, UserName = username, LoginKey = loginKey, Level = level, GameId = gameid, SessionId = sessionid, Status = GameEntry.GameStatuses.Open });

                        return fastJSON.JSON.Instance.ToJSON(result);
                    }
                case MesserSmashWeb.REQUEST_END_GAME: {
                        GameStates states = _server.handleSaveGame(rawData);
                        if (states != null) {
                            var ticks = saveGameAndGetTimestamp(states);
                            _gameEntries.addEntry(new GameEntry {UserId = states.UserId, GameId=states.GameId, Level = (uint)states.Level, SessionId=states.SessionId, Ticks = ticks, UserName = states.UserName, LoginKey=states.LoginKey, Status = GameEntry.GameStatuses.Closed });
                        } else {
                            return string.Format("Foobar | error={0}", states);
                        }
                        return "OK";
                    }
                case MesserSmashWeb.REQUEST_GET_HIGHSCORE_ON_LEVEL: {
                        try {
                            var items = toTable(rawData);
                            uint level = uint.Parse(items[MesserSmashWeb.LEVEL].ToString());
                            var scores = _highscores.getHighscoresOnLevel(level).OrderByDescending(a => a.Score);
                            //return fastJSON.JSON.Instance.ToJSON(scores);
                            var sb = new StringBuilder();
                            foreach (var item in scores) {
                                sb.AppendLine(item.ToString());
                            }
                            return sb.ToString();
                        } catch (Exception e) {
                            Console.WriteLine("Unable to parse data from client {0}", e.ToString());
                            Logger.error("Unable to parse data from client {0}", e.ToString());
                        }

                    }
                    break;
                case MesserSmashWeb.REQUEST_GET_HIGHSCORE_FOR_ROUND: {
                        try {
                            var items = toTable(rawData);
                            uint round = uint.Parse(items[MesserSmashWeb.ROUND_ID].ToString());
                            var scores = _highscores.getHighscoresOnRound(round);
                            var sb = new StringBuilder();
                            foreach (var item in scores.Values) {
                                sb.AppendLine(item.ToString());
                            }
                            return sb.ToString();
                        } catch (Exception e) {
                            Console.WriteLine("Unable to parse data from client {0}", e.ToString());
                            Logger.error("Unable to parse data from client {0}", e.ToString());
                        }

                    }
                    break;
                case MesserSmashWeb.REQUEST_CONTINUE_GAME: {
                        var items = toTable(rawData);
                        uint level = uint.Parse(items[MesserSmashWeb.LEVEL].ToString());
                        string session = items[MesserSmashWeb.SESSION_ID].ToString();
                        string userid = items[MesserSmashWeb.USER_ID].ToString();
                        string username = items[MesserSmashWeb.USER_NAME].ToString();
                        string loginkey = items[MesserSmashWeb.LOGIN_SESSION].ToString();

                        var gameid = Guid.NewGuid().ToString();
                        _gameEntries.addEntry(new GameEntry { UserId = userid, UserName = username, LoginKey = loginkey, Level = level, GameId = gameid, SessionId = session, Status = GameEntry.GameStatuses.Open });

                        var result = new Dictionary<string, object> {
                            {MesserSmashWeb.GAME_ID, gameid},
                            {MesserSmashWeb.SESSION_ID, session}
                        };
                        return fastJSON.JSON.Instance.ToJSON(result);
                    }
                default: {
                        Logger.error("Unhandled request: {0}", request);
                        var jsondata = fastJSON.JSON.Instance.ToObject<Dictionary<string, object>>(rawData);
                    }
                    break;
            }
            
            return "status=666";
        }

        private static Dictionary<string, object> toTable(string rawData) {
            if (rawData == "" || rawData == null) { return new Dictionary<string, object>(); }
            return fastJSON.JSON.Instance.Parse(rawData) as Dictionary<string, object>;
        }

        private static long saveGameAndGetTimestamp(GameStates data) {
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
            _highscores.addHighscore(new Highscore { Ticks = timestamp, File = timestamp + "_save.txt", GameId = data.GameId, GameVersion = data.GameVersion, Kills = (uint)data.Kills, Level = (uint)data.Level, Score = (uint)data.Score, SessionId = data.SessionId, UserId = data.UserId, UserName = data.UserName });
            return timestamp;
        }
    }
}

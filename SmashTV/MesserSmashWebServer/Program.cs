﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using SharedSmashResources;
using MesserSmash.Modules;

namespace MesserSmashWebServer {
    class Program {
        private static StreamWriter _highscores;
        private static string _url;
        //http://pawncraft.co.uk:8801/ this is the external url to the messersmash server

        static void Main(string[] args) {
            _highscores = new StreamWriter("highscores.txt", true);
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
            var server = new MesserSmashWebServer(SendResponse, url);
            server.Run();
            Console.WriteLine("A simple webserver url:{0} \nPress a key to quit.", url);
            Console.ReadKey();
            server.Stop();
        }

        public static string SendResponse(HttpListenerRequest request) {
            var timestamp = DateTime.Now;
            StatusUpdate states = null;
            var server = new LocalServer(_url);
            try {
                var data = new byte[request.ContentLength64];
                if (!request.HasEntityBody) { return null; }
                using (System.IO.Stream body = request.InputStream) {
                    body.Read(data, 0, data.Length);
                }                
                states = server.receive(data);
            } catch (Exception e) {
                Console.WriteLine("Unable to parse data from client {0}", e.ToString());
                Logger.error("Unable to parse data from client {0}", e.ToString());
            }
            if (states != null) {
                saveGame(states);
            } else {
                return string.Format("Foobar | error={0}", states);
            }
            StringBuilder sb = new StringBuilder();            
            sb.AppendFormat("{1}->Handled request in: {0}ms from user={2}[{3}]", (DateTime.Now - timestamp).TotalMilliseconds, DateTime.Now.ToString("dd/MM[HH:mm:ss]"), states.UserName, states.UserId);
            Console.WriteLine(sb.ToString());
            Logger.info(sb.ToString());
            return string.Format("Foobar | status={0}", states);
        }

        private static void saveGame(StatusUpdate data) {
            var folder = new DirectoryInfo(System.Environment.CurrentDirectory);
            folder = folder.CreateSubdirectory("games");

            var timestamp = DateTime.Now.Ticks.ToString();
            using (var sw = new StreamWriter(folder.FullName + "/" + timestamp + "_save.txt")) {
                sw.Write(data.toJson());
                sw.Flush();
            }

            using (var sw = new StreamWriter(folder.FullName + "/last_save.txt", false)) {
                sw.Write(data.toJson());
                sw.Flush();
            }
            _highscores.WriteLine("ticks={0}|username={1}|level={6}|userid={2}|gameid={3}|kills={4}|score={5}|version={7}", timestamp, data.UserName, data.UserId, data.GameId, data.Kills, data.Score, data.Level, data.GameVersion);
            _highscores.Flush();
        }
    }
}

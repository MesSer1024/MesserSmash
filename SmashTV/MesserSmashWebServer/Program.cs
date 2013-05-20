using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MesserSmashWebServer {
    class Program {
        //http://pawncraft.co.uk:8801/ this is the external url to the messersmash server

        static void Main(string[] args) {
            string url = null;
            using (var sr = new StreamReader("./config.ini")) {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine().Trim();
                    var splitter = '|';
                    if (line.StartsWith("local_url")) {
                        url = line.Split(splitter)[1].Trim();
                    }
                }
            }
            url = url ?? "http://pawncraft.co.uk:8801/";
            var server = new MesserSmashWebServer(SendResponse, url);
            server.Run();
            Console.WriteLine("A simple webserver url:{0} \nPress a key to quit.", url);
            Console.ReadKey();
            server.Stop();
        }

        public static string SendResponse(HttpListenerRequest request) {
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
        }
    }
}

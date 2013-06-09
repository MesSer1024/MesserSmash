using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class RequestHighscoresCommand : Command {
        public const string NAME = "RequestHighscoresCommand";

        public int Level { get; private set; }

        public RequestHighscoresCommand(int level)
            : base(NAME) {

            Level = level;

            ThreadWatcher.runBgThread(() => {
                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                var dir = new Dictionary<string, object> {
                    {MesserSmashWeb.LEVEL, level},
                    {MesserSmashWeb.LOGIN_SESSION, SmashTVSystem.Instance.LoginResponseKey},
                };
                server.requestHighscores(onResponse, dir);
            });
        }

        private void onResponse(int rc, string data) {
            if (rc == 0) {
                using (StringReader sr = new StringReader(data)) {
                    SmashTVSystem.Instance.HighscoresClient.clearData((uint)Level);
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        SmashTVSystem.Instance.HighscoresClient.addHighscore(Highscore.FromString(line));
                    }
                }
            }            
        }
    }
}

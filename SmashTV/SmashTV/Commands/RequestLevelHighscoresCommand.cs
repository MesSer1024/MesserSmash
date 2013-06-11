using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class RequestLevelHighscoresCommand : Command {
        public const string NAME = "RequestLevelHighscoresCommand";
        private Action<RequestLevelHighscoresCommand> _callback;

        public int Level { get; private set; }
        public HighscoreContainer ScoringProvider { get; set; }

        public RequestLevelHighscoresCommand(int level, HighscoreContainer scoringProvider, Action<RequestLevelHighscoresCommand> cb)
            : base(NAME) {

            _callback = cb;
            Level = level;
            ScoringProvider = scoringProvider;
            ScoringProvider.clearData();
            //Hack in local highscores...
            var localScores = LocalHighscore.Instance.getHackedHighscoreList(level);
            ScoringProvider.addHighscores(localScores);

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
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        var score = Highscore.FromString(line);
                        //#TODO: Fix these sometime...
                        score.IsLocalHighscore = false;
                        score.IsVerified = true;
                        ScoringProvider.addHighscore(score);
                    }
                }
            }
            if (_callback != null) {
                _callback.Invoke(this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class RequestRoundHighscoresCommand : Command {
        public const string NAME = "RequestRoundHighscoresCommand";

        private Action<RequestRoundHighscoresCommand> _callback;
        public uint RoundId { get; private set; }
        private HighscoreContainer _scoringProvider;
        public List<Highscore> Scores { get; private set; }

        public RequestRoundHighscoresCommand(uint roundid, HighscoreContainer scoringProvider, Action<RequestRoundHighscoresCommand> cb)
            : base(NAME) {

            _callback = cb;
            Scores = new List<Highscore>();
            RoundId = roundid;
            _scoringProvider = scoringProvider;
            _scoringProvider.clearData();
            //Hack in local highscores...
            var localScores = LocalHighscore.Instance.getHackedHighscoreListForRound(roundid);
            _scoringProvider.addHighscores(localScores);

            ThreadWatcher.runBgThread(() => {
                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                var dir = new Dictionary<string, object> {
                        {MesserSmashWeb.VERIFIED_LOGIN_SESSION, SmashTVSystem.Instance.LoginResponseKey},
                        {MesserSmashWeb.ROUND_ID, SmashTVSystem.Instance.RoundId},
                    };
                server.requestRoundHighscores(onRoundResponse, dir);
            });
        }

        private void onRoundResponse(int rc, string data) {
            if (rc == 0) {
                using (StringReader sr = new StringReader(data)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        var score = Highscore.FromString(line);
                        if (score != null) {
                            //#TODO: Fix these sometime...
                            score.IsLocalHighscore = false;
                            score.IsVerified = true;
                            Scores.Add(score);
                        }
                    }
                }
            }
            if (_callback != null)
                _callback.Invoke(this);
        }
    }
}

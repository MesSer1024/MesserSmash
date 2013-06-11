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
        public HighscoreContainer ScoringProvider { get; private set; }

        public RequestRoundHighscoresCommand(uint roundid, HighscoreContainer scoringProvider, Action<RequestRoundHighscoresCommand> cb)
            : base(NAME) {

            _callback = cb;
            RoundId = roundid;
            ScoringProvider = scoringProvider;
            ScoringProvider.clearData();
            //Hack in local highscores...
            var localScores = LocalHighscore.Instance.getHackedHighscoreListForRound(roundid);
            ScoringProvider.addHighscores(localScores);

            ThreadWatcher.runBgThread(() => {
                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                var dir = new Dictionary<string, object> {
                        {MesserSmashWeb.LOGIN_SESSION, SmashTVSystem.Instance.LoginResponseKey},
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
                        //#TODO: Fix these sometime...
                        score.IsLocalHighscore = false;
                        score.IsVerified = true;
                        ScoringProvider.addHighscore(score);
                    }
                }
            }
            if (_callback != null)
                _callback.Invoke(this);
        }
    }
}

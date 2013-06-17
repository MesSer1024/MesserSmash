using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class RoundWonCommand : Command {
        public const string NAME = "RoundWonCommand";
        private int ScoreOnLevel;
        public SmashTVSystem GameInstance { get; set; }
        public GUI.GUIMain Gui { get; set; }
        
        public uint RoundId { get; private set; }

        public RoundWonCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
            RoundId = SmashTVSystem.Instance.RoundId;
            ScoreOnLevel = (int)Scoring.getLevelScore();
            Gui.showEntireRoundWon(GameInstance.GameHighscores, false, ScoreOnLevel);
            new RequestRoundHighscoresCommand(GameInstance.RoundId, GameInstance.GameHighscores, onRoundResponse).execute();
        }

        private void onRoundResponse(RequestRoundHighscoresCommand cmd) {
            if (!SmashTVSystem.IsGameStarted) {
                var scoringProvider = SmashTVSystem.Instance.GameHighscores;
                scoringProvider.addHighscores(cmd.Scores);
                Gui.showEntireRoundWon(scoringProvider, true, ScoreOnLevel);
            } else {
                Logger.error("Game started before LevelWonCommand was finished with highscores");
            }

        }


    }
}

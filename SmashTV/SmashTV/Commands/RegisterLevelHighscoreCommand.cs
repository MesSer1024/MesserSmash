using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using MesserSmash.Modules;
using System.Diagnostics;

namespace MesserSmash.Commands {
    class RegisterLevelHighscoreCommand : Command {
        public const string NAME = "RegisterLevelHighscoreCommand";

        public RegisterLevelHighscoreCommand(string playerName, int level)
            : base(NAME) {
            // TODO: Complete member initialization
            var levelScore = Scoring.getLevelScores();
            var scoringData = levelScore[levelScore.Count - 1];
            Highscore.Instance.insertLevelHighscore(playerName, scoringData.Level, scoringData.Score, scoringData.Kills);
            Logger.info("{0} level{1} - score: {2}, kills: {3}", playerName, level, scoringData.Score, scoringData.Kills);
        }
    }
}

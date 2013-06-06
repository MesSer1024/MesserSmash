﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using MesserSmash.Modules;
using System.Diagnostics;

namespace MesserSmash.Commands {
    class RegisterHighscoreCommand : Command {
        public const string NAME = "RegisterHighscoreCommand";        

        public RegisterHighscoreCommand(string playerName) : base(NAME) {
            // TODO: Complete member initialization
            var levelScore = Scoring.getLevelScores();
            var totalScore = 0;
            var totalkills = 0;
            foreach (var i in levelScore) {                
                totalScore += i.Score;
                totalkills += i.Kills;
                ClientHighscore.Instance.resetWithLevelInsertAndOutputToFile(playerName, i.Level, i.Score, i.Kills);
            }
            Logger.info("RegisterHighscoreCommand: {0} - {1} - {2}", playerName, totalScore, totalkills);
            ClientHighscore.Instance.resetWithTotalScoreAndInsertScoreAndOutputToFile(playerName, totalScore, totalkills);
        }
    }
}

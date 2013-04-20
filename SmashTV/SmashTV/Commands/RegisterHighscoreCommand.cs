using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using MesserSmash.Modules;
using System.Diagnostics;

namespace MesserSmash.Commands {
    class RegisterHighscoreCommand : Command {
        public static string NAME = "RegisterHighscoreCommand";        

        public RegisterHighscoreCommand(string playerName) : base(NAME) {
            // TODO: Complete member initialization
            Logger.info("{0} - {1} - {2}", playerName, (int)Scoring.getScore(), (int)Scoring.getKills());
            Highscore.Instance.insert(playerName, (int)Scoring.getScore(), (int)Scoring.getKills());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash {
    class LevelScore {
        public int Level {get; private set;}
        public int Kills { get; set; }
        public int Score { get; set; }

        public LevelScore(int level) {
            Level = level;
        }
    }
}

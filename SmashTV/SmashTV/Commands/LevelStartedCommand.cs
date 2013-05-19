using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Arenas;

namespace MesserSmash.Commands {
    class LevelStartedCommand : Command {
        public const string NAME = "LevelStartedCommand";

        public LevelStartedCommand(Arena level):base(NAME) {
            // TODO: Complete member initialization
            Level = level;
        }

        public Arena Level { get; set; }
    }
}

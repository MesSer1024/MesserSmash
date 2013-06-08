using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using SharedSmashResources;
using System.IO;

namespace MesserSmash.Commands {
    class RestartGameCommand : Command {
        public const string NAME = "RestartGameCommand";
        public int Level { get; private set; }

        public RestartGameCommand(int level = 1)
            : base(NAME) {
                Level = level;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class RestartGameCommand : Command {
        public static readonly string NAME = "StartGameCommand";

        public RestartGameCommand(int level = 1)
            : base(NAME) {
                Level = level;
        }

        public int Level { get; private set; }
    }
}

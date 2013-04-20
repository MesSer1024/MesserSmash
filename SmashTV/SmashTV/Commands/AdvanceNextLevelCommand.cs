using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class AdvanceNextLevelCommand : Command {
        public static string NAME = "AdvanceNextLevelCommand";

        public AdvanceNextLevelCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
        }
    }
}

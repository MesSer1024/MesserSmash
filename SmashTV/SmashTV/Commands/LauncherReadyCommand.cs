using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.GUI;

namespace MesserSmash.Commands {
    class LauncherReadyCommand : Command {
        public const string NAME = "LauncherReadyCommand";

        public LauncherReadyCommand()
            : base(NAME) {
                var cmd = new PlaySoundCommand(AssetManager.getWeaponReadySound());
                cmd.Volume = 0.6f;
                cmd.execute();
        }
    }
}

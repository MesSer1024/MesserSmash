using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MesserSmash.Commands {
    class PlaySoundCommand : Command {
        public static readonly string NAME = "FirePistolShotCommand";

        public PlaySoundCommand(SoundEffect soundEffect):base(NAME) {
            // TODO: Complete member initialization
            Sound = soundEffect;
        }

        public SoundEffect Sound { get; set; }
    }
}

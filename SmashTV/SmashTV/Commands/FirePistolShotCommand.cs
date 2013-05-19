using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MesserSmash.Commands {
    class PlaySoundCommand : Command {
        public const string NAME = "PlaySoundCommand";

        public PlaySoundCommand(SoundEffect soundEffect):base(NAME) {
            // TODO: Complete member initialization
            Sound = soundEffect;
            Volume = 0.25f;
        }

        public SoundEffect Sound { get; set; }
        public float Volume { get; set; }
    }
}

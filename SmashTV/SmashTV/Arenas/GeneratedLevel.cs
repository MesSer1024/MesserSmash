using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Arenas
{
    class GeneratedLevel : Arena
    {
        public GeneratedLevel(MesserSmash.Arenas.LevelBuilder.jsLevel data)
        {
            this.Level = data.Level;
            this._secondsLeft = (float)data.Time;
            this._spawners = data.Waves;
        }
    }
}

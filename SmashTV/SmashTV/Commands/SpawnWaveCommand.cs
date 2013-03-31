using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Commands {
    class SpawnWaveCommand : Command {
        public static readonly string NAME = "SpawnWaveCommand";

        public SpawnWaveCommand(WaveSpawner spawner):base(NAME) {
            WaveSpawner = spawner;            
        }

        public WaveSpawner WaveSpawner { get; private set; }
    }
}

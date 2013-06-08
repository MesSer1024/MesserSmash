using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Commands {
    class SpawnWaveCommand : Command {
        public const string NAME = "SpawnWaveCommand";

        public SpawnWaveCommand(WaveSpawner spawner):base(NAME) {
            executeDirectly = true;
            WaveSpawner = spawner;            
        }

        public WaveSpawner WaveSpawner { get; private set; }
    }
}

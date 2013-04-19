using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class LootPickedUpCommand : Command {
        public static readonly string NAME = "LootPickedUpCommand";

        public LootPickedUpCommand(Loot loot, Player player)
            : base(NAME) {
                LootReached = loot;
                PlayerLooter = player;
                loot.inactivate();
        }

        public Player PlayerLooter { get; private set; }
        public Loot LootReached { get; private set; }
    }
}

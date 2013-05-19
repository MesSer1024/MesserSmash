using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Enemies;

namespace MesserSmash.Commands {
    class PlayerDiedCommand  : Command {
        public const string NAME = "PlayerDiedCommand";
        private MesserSmash.Player _player;

        public PlayerDiedCommand(Player player)
            : base(NAME) {
            _player = player;
        }

        public Player Player {
            get { return _player; }
        }
    }
}

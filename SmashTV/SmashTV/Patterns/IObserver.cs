using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace SharedSmashResources.Patterns {
    public interface IObserver {
        void handleCommand(ICommand cmd);
    }
}

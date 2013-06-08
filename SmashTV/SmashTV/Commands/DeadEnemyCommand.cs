// -----------------------------------------------------------------------
// <copyright file="DeadEnemy.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Enemies;

namespace MesserSmash.Commands {
    public class DeadEnemyCommand : Command {
        public const string NAME = "DeadEnemyCommand";

        public DeadEnemyCommand(IEnemy enemy, float amount = 0) : base(NAME) {
            executeDirectly = true;
            Enemy = enemy;
        }


        public IEnemy Enemy { get; private set; }
    }
}

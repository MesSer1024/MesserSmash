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
        public static readonly string NAME = "DeadEnemyCommand";

        public DeadEnemyCommand(IEnemy enemy, float amount = 0) : base(NAME) {
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.EnemyKilled, this, "damage:" + amount);
            Enemy = enemy;
        }


        public IEnemy Enemy { get; private set; }
    }
}

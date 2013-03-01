// -----------------------------------------------------------------------
// <copyright file="EnemyFactory.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Microsoft.Xna.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnemyFactory {
        public void create(Vector2 position) {
            var m = new MeleeEnemy(position, SmashTVSystem.Instance.Player);
            m.init();
        }
    }
}

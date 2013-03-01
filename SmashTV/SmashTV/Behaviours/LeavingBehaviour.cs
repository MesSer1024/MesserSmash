// -----------------------------------------------------------------------
// <copyright file="LeavingBehaviour.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using MesserSmash.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Behaviours {


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LeavingBehaviour :Behaviour {

        protected override void onUpdate() {
            try {
                (Enemy as EnemyBase).Health = float.MaxValue;
                if (TimeActive >= 3.0f) {
                    notifyDone();
                }
            }
            catch (Exception e) {
                //TODO Create a debugger/logger
                //Debug.
            }
        }
    }
}

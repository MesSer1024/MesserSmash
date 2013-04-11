// -----------------------------------------------------------------------
// <copyright file="NullBehaviour.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MesserSmash.Behaviours {
    public class NullBehaviour : Behaviour {
        public NullBehaviour() {
            PathFindEnabled = false;
        }

        protected override void onUpdate() {}
    }
}

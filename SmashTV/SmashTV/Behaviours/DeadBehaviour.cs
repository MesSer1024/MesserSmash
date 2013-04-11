
namespace MesserSmash.Behaviours {

    public class DeadBehaviour : Behaviour{
        public DeadBehaviour() {
            PathFindEnabled = false;
        }

        protected override void onUpdate() {
            if (TimeThisBehaviour >= 1.5f) {
                notifyDone();
            }
        }
    }
}

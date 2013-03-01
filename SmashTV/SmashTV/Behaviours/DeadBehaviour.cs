
namespace MesserSmash.Behaviours {

    public class DeadBehaviour : Behaviour{
        public DeadBehaviour() {
            CollisionEnabled = false;
        }

        protected override void onUpdate() {
            if (TimeActive >= 1.5f) {
                notifyDone();
            }
        }
    }
}

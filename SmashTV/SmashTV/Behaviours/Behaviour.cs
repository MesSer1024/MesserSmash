using Microsoft.Xna.Framework;
using MesserSmash.Enemies;

namespace MesserSmash.Behaviours {
    public class Behaviour {
        public delegate void BehaviourDelegate(Behaviour behaviour);

        public event BehaviourDelegate onBehaviourEnded;

        public Player Target { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public IEnemy Enemy { get; set; }

        public bool PathFindEnabled { get; protected set; }
        public bool CollisionEnabled { get { return PathFindEnabled; } }

        public float TimeThisBehaviour { get; private set; }
        protected float DeltaTime { get; private set; }



        public Behaviour() {
            SmashTVSystem.Instance.behaviourCreated();
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            PathFindEnabled = true;
        }

        public void update(float deltatime) {
            DeltaTime = deltatime;
            TimeThisBehaviour += deltatime;
            onUpdate();
        }

        protected virtual void onUpdate() { }

        protected void notifyDone() {
            if(onBehaviourEnded != null)
                onBehaviourEnded.Invoke(this);
        }


        ~Behaviour() {
            SmashTVSystem.Instance.behaviourRemoved();
        }
    }
}
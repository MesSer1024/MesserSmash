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

        public bool CollisionEnabled { get; protected set; }

        protected float TimeActive { get; private set; }
        protected float DeltaTime { get; private set; }



        public Behaviour() {
            SmashTVSystem.Instance.behaviourCreated();
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            CollisionEnabled = true;
        }

        public void update(float deltatime) {
            DeltaTime = deltatime;
            TimeActive += deltatime;
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
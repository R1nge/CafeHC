using UnityEngine;

namespace AI
{
    public abstract class State
    {
        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void OnTriggerEnter(Collider other) { }
    }
}
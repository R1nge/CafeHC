using UnityEngine;

namespace AI
{
    //TODO: use a base class instead of interface???
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void OnTriggerEnter(Collider other);

        void OnTriggerStay(Collider other);
    }
}
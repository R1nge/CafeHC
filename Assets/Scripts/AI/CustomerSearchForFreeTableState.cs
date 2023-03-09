using UnityEngine;

namespace AI
{
    public class CustomerSearchForFreeTableState : IState
    {
        public void Enter()
        {
            Debug.Log("FIND FREE TABLE");
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
        }
    }
}
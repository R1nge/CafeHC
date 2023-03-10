using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerAtCounterState : IState
    {
        private readonly CustomerStateManager _stateManager;

        public CustomerAtCounterState(CustomerStateManager customerStateManager )
        {
            _stateManager = customerStateManager;
        }

        public void Enter()
        {
            //TODO: make order
            Debug.Log("AT COUNTER");
            _stateManager.SetCustomerSearchForFreeTable();
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
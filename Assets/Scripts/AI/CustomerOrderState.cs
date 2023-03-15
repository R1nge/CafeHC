using UnityEngine;

namespace AI
{
    public class CustomerOrderState : IState
    {
        private readonly CustomerInventoryUI _customerInventoryUI;

        public CustomerOrderState(CustomerInventoryUI inventoryUI)
        {
            _customerInventoryUI = inventoryUI;
        }

        public void Enter()
        {
            _customerInventoryUI.UpdateUI();
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
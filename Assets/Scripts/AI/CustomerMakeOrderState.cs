using UnityEngine;

namespace AI
{
    public class CustomerMakeOrderState : IState
    {
        private readonly CustomerInventoryUI _customerInventoryUI;

        public CustomerMakeOrderState(CustomerInventoryUI inventoryUI)
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

        public void OnTriggerStay(Collider other)
        {
        }
    }
}
using UnityEngine;

namespace AI
{
    public class CustomerOrderState : IState
    {
        private readonly Inventory _inventory;
        private readonly CustomerInventoryUI _customerInventoryUI;

        public CustomerOrderState( Inventory inventory, CustomerInventoryUI inventoryUI)
        {
            _inventory = inventory;
            _customerInventoryUI = inventoryUI;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _customerInventoryUI.UpdateUI(_inventory.GetMaxAmount() - _inventory.GetCount());
        }

        public void OnTriggerEnter(Collider other)
        {
        }
    }
}
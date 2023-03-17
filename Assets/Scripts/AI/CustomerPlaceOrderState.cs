using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerPlaceOrderState : IState
    {
        private readonly CustomerStateManager _customerStateManager;
        private readonly CustomerInventory _customerInventory;
        private bool _done;

        public CustomerPlaceOrderState(CustomerStateManager customerStateManager, CustomerInventory customerInventory)
        {
            _customerStateManager = customerStateManager;
            _customerInventory = customerInventory;
        }

        public void Enter()
        {
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
            if (_done) return;
            if (other.TryGetComponent(out TableInventory tableInventory))
            {
                if (other.TryGetComponent(out Table table))
                {
                    table.GetFreeSeat().SetCustomer(_customerStateManager);
                }

                var count = _customerInventory.GetCount();
                for (int i = 0; i < count; i++)
                {
                    _customerInventory.TryTransferTo(tableInventory);
                }

                _done = true;
            }
        }
    }
}
using UnityEngine;

namespace AI
{
    public class CustomerAtCounterState : IState
    {
        private CustomerOrder _customerOrder;
        private readonly CustomerStateManager _stateManager;
        private readonly CustomerMovement _customerMovement;
        private readonly Waypoints _waypoints;

        public CustomerAtCounterState(CustomerStateManager customerStateManager, CustomerMovement customerMovement,
            Waypoints waypoints)
        {
            _stateManager = customerStateManager;
            _customerMovement = customerMovement;
            _waypoints = waypoints;
        }

        public void Enter()
        {
            _customerOrder = new CustomerOrder(_customerMovement);
            _customerOrder.MakeOrder();
            //TODO: make order
            Debug.Log("AT COUNTER");
            _stateManager.SetCustomerSearchForFreeTable();
        }

        public void Exit()
        {
            _waypoints.RemoveCustomer();
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
        }
    }
}
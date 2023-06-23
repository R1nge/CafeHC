using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerStateManager : MonoBehaviour
    {
        private TableManager _tableManager;
        private Waypoints _waypoints;
        private CustomerMovement _movement;
        private Inventory _inventory;
        private Collider _collider;

        private State _inQueue;
        private State _makeOrder;
        private State _searchForFreeTable;
        private State _takeTable;
        private State _goHome;

        private State _currentState;

        private void Awake()
        {
            _tableManager = FindObjectOfType<TableManager>();
            _waypoints = FindObjectOfType<Waypoints>();
            _movement = GetComponent<CustomerMovement>();
            _inventory = GetComponent<Inventory>();
            _collider = GetComponent<Collider>();

            _inQueue = new CustomerInQueue(this, _movement);
            _makeOrder = new CustomerMakeOrder(_inventory);
            _searchForFreeTable = new CustomerSearchForFreeTable(this, _movement, _tableManager, _collider);
            _takeTable = new CustomerTakeTable(_inventory, _collider);
            _goHome = new CustomerGoHome(_movement, _waypoints.Home());

            SetState(_inQueue);
        }

        public void MakeOrder() => SetState(_makeOrder);

        public void SearchForFreeTable() => SetState(_searchForFreeTable);

        public void TakeTable() => SetState(_takeTable);

        public void GoHome() => SetState(_goHome);

        private void SetState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void Update() => _currentState?.Update();

        private void OnTriggerEnter(Collider other) => _currentState.OnTriggerEnter(other);
    }
}
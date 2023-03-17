using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerSearchForFreeTableState : IState
    {
        private readonly CustomerStateManager _customerStateManager;
        private readonly TableManager _tableManager;
        private readonly CustomerMovement _customerMovement;
        private readonly Waypoints _waypoints;
        private bool _hasFoundTable;
        private Vector3 _desiredPosition;

        public CustomerSearchForFreeTableState(CustomerStateManager customerStateManager, TableManager tableManager,
            CustomerMovement customerMovement,
            Waypoints waypoints)
        {
            _customerStateManager = customerStateManager;
            _tableManager = tableManager;
            _customerMovement = customerMovement;
            _waypoints = waypoints;
        }

        public void Enter()
        {
            if (TryFindTable())
            {
                _hasFoundTable = true;
            }
        }

        public void Exit()
        {
        }

        public void Update()
        {
            if (_hasFoundTable)
            {
                if (Vector3.Distance(_customerMovement.transform.position, _desiredPosition) <= 1f)
                {
                    _customerStateManager.SetCustomerPlaceOrder();
                }

                return;
            }

            if (TryFindTable())
            {
                _hasFoundTable = true;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
        }

        public void OnTriggerStay(Collider other)
        {
        }

        private bool TryFindTable()
        {
            if (_tableManager.GetFreeTable() == null) return false;
            if (!_tableManager.GetFreeTable().HasFreeSeat()) return false;
            _waypoints.RemoveCustomer();
            var pos = _tableManager.GetFreeTable().GetFreeSeat().GetPosition();
            _customerMovement.MoveTo(pos);
            _desiredPosition = pos;
            return true;
        }
    }
}
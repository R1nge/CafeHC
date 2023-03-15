using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerSearchForFreeTableState : IState
    {
        private readonly TableManager _tableManager;
        private readonly CustomerMovement _customerMovement;
        private readonly Waypoints _waypoints;
        private bool _hasFoundTable;

        public CustomerSearchForFreeTableState(TableManager tableManager, CustomerMovement customerMovement,
            Waypoints waypoints)
        {
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
            if (_hasFoundTable) return;
            if (TryFindTable())
            {
                _hasFoundTable = true;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
        }

        private bool TryFindTable()
        {
            if (_tableManager.GetFreeTable() == null) return false;
            if (!_tableManager.GetFreeTable().HasFreeSeat()) return false;
            _waypoints.RemoveCustomer();
            _customerMovement.MoveTo(_tableManager.GetFreeTable().GetFreeSeat().GetPosition());
            return true;
        }
    }
}
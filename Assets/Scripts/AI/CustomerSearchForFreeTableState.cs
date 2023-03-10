using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerSearchForFreeTableState : IState
    {
        private readonly TableManager _tableManager;
        private readonly CustomerMovement _customerMovement;
        private readonly Waypoints _waypoints;

        public CustomerSearchForFreeTableState(TableManager tableManager, CustomerMovement customerMovement, Waypoints waypoints)
        {
            _tableManager = tableManager;
            _customerMovement = customerMovement;
            _waypoints = waypoints;
        }

        public void Enter()
        {
            if (_tableManager.GetFreeTable() == null) return;
            if (!_tableManager.GetFreeTable().HasFreeSeat()) return;
            _customerMovement.MoveTo(_tableManager.GetFreeTable().GetFreeSeat().GetPosition());
            _waypoints.RemoveCustomer();
            Debug.Log("FOUND FREE TABLE");
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
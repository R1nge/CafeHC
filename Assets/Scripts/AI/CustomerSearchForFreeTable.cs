using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerSearchForFreeTable : State
    {
        private readonly CustomerStateManager _customerStateManager;
        private readonly CustomerMovement _movement;
        private readonly TableManager _tableManager;
        private readonly Collider _collider;
        private Table _selectedTable;
        private Vector3 _tablePosition;

        public CustomerSearchForFreeTable(
            CustomerStateManager customerStateManager,
            CustomerMovement movement,
            TableManager tableManager,
            Collider collider)
        {
            _customerStateManager = customerStateManager;
            _movement = movement;
            _tableManager = tableManager;
            _collider = collider;
        }

        public override void Enter()
        {
            _selectedTable = _tableManager.GetFreeTable();
            
            if (_selectedTable == null)
            {
                _tableManager.OnFreeUpEvent += MoveToTable;
                Debug.LogError($"Customer search for a free table: table is null");
            }
            else
            {
                MoveToTable(_selectedTable);
            }
        }
        
        private void MoveToTable(Table table)
        {
            _selectedTable = table;
            _tablePosition = _selectedTable.GetFreeSeat().GetPosition();
            _selectedTable.GetFreeSeat().SetCustomer(_customerStateManager);
            _movement.MoveTo(_tablePosition);
            _movement.RemoveFromQueue();
            _collider.enabled = false;
        }

        public override void Update()
        {
            if (Vector3.Distance(_customerStateManager.transform.position, _tablePosition) <= 0.6f)
            {
                _customerStateManager.TakeTable();
            }
        }

        public override void Exit()
        {
            _tableManager.OnFreeUpEvent -= MoveToTable;
        }
    }
}
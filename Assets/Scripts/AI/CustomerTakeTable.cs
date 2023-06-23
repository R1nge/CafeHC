using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerTakeTable : State
    {
        private readonly Inventory _inventory;
        private readonly Collider _collider;

        public CustomerTakeTable(Inventory inventory, Collider collider)
        {
            _inventory = inventory;
            _collider = collider;
        }

        public override void Enter() => _collider.enabled = true;

        public override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TableInventory tableInventory))
            {
                var count = _inventory.GetCount();
                for (int i = 0; i < count; i++)
                {
                    _inventory.TryTransferTo(tableInventory);
                }
            }
        }
    }
}
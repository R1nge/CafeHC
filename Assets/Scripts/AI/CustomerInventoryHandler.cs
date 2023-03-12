using UnityEngine;

namespace AI
{
    public class CustomerInventoryHandler : MonoBehaviour
    {
        private Inventory _inventory;
        private CustomerStateManager _stateManager;
        private CustomerInventoryUI _inventoryUI;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += InventoryOnItemAdded;
            _stateManager = GetComponent<CustomerStateManager>();
            _inventoryUI = GetComponent<CustomerInventoryUI>();
        }

        private void Start() => _inventory.SetMaxAmount(Random.Range(5, 10));

        private void InventoryOnItemAdded(InventoryItem item)
        {
            if (_inventory.IsFull())
            {
                _stateManager.SetCustomerSearchForFreeTable();
            }
            
            _inventoryUI.UpdateUI(_inventory.GetMaxAmount() - _inventory.GetCount());
        }

        private void OnDestroy()
        {
            _inventory.OnItemAddedEvent -= InventoryOnItemAdded;
        }
    }
}
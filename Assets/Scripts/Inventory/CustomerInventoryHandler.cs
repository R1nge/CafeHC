using AI;
using UnityEngine;

public class CustomerInventoryHandler : InventoryHandler
{
    private CustomerStateManager _stateManager;

    protected override void Awake()
    {
        base.Awake();
        Inventory.SetSize(Random.Range(5, 10));
        _stateManager = GetComponent<CustomerStateManager>();
    }

    protected override void OnItemAdded(InventoryItem item)
    {
        item.GetFromPool(transform.position, Quaternion.identity, transform);
        
        if (Inventory.IsFull())
        {
            _stateManager.SetCustomerSearchForFreeTable();
        }
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        item.ReturnToPool(transform.GetChild(transform.childCount - 1).gameObject);
    }
}
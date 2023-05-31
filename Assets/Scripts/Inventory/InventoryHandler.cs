using UnityEngine;

public abstract class InventoryHandler : MonoBehaviour
{
    protected Inventory Inventory;

    protected virtual void Awake()
    {
        Inventory = GetComponent<Inventory>();
        Inventory.OnItemAddedEvent += OnItemAdded;
        Inventory.OnItemRemovedEvent += OnItemRemoved;
        Inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
    }

    protected abstract void OnItemAdded(InventoryItem item);

    protected abstract void OnItemRemoved(InventoryItem item);
    
    protected virtual void OnAllItemsRemoved(InventoryItem item)
    {
    }

    private void OnDestroy()
    {
        Inventory.OnItemAddedEvent -= OnItemAdded;
        Inventory.OnItemRemovedEvent -= OnItemRemoved;
        Inventory.OnAllItemsRemovedEvent -= OnAllItemsRemoved;
    }
}
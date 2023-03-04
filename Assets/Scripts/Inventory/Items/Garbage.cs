using Pickupable;
using UnityEngine;

public class Garbage : MonoBehaviour, IPickupable
{
    private GarbageItem _item = new();

    private void Awake()
    {
        _item.itemType = InventoryItem.ItemType.Garbage;
    }

    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(_item))
        {
            inventory.ReturnToPool(_item, gameObject);
        }
    }
}
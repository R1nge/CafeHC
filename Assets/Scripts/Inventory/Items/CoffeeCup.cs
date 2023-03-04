using Pickupable;
using UnityEngine;

public class CoffeeCup : MonoBehaviour, IPickupable
{
    private CoffeeItem _item = new();

    private void Awake()
    {
        _item.itemType = InventoryItem.ItemType.Coffee;
    }

    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(_item))
        {
            inventory.ReturnToPool(_item, gameObject);
        }
    }
}
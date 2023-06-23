using UnityEngine;

[RequireComponent(typeof(PlayerInventoryHandler))]
public class PlayerInventory : Inventory
{
    public void Initialize()
    {
        var coffee = ItemManager.GetItem(InventoryItemType.Coffee);
        var garbage = ItemManager.GetItem(InventoryItemType.Garbage);
        AllowedItems = new(2)
        {
            { coffee.ItemType(), coffee },
            { garbage.ItemType(), garbage }
        };
    }
}
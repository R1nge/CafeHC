using UnityEngine;

[RequireComponent(typeof(PlayerInventoryHandler))]
public class PlayerInventory : Inventory
{
    public void Initialize()
    {
        var coffee = ItemManager.GetItem(InventoryItemType.Coffee);
        AllowedItems = new(1)
        {
            { coffee.ItemType(), coffee }
        };
    }
}
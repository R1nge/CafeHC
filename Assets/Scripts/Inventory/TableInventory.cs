using AI;
using UnityEngine;

[RequireComponent(typeof(TableInventorHandler))]
public class TableInventory : Inventory
{
    private void Start()
    {
        var coffee = ItemManager.GetItem(InventoryItemType.Coffee);
        AllowedItems = new(1)
        {
            { coffee.ItemType(), coffee }
        };
    }
}
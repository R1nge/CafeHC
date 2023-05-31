using UnityEngine;

[RequireComponent(typeof(CounterInventoryHandler))]
public class CounterInventory : Inventory
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
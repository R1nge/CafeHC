using UnityEngine;

[RequireComponent(typeof(CoffeeMachineInventoryHandler))]
public class CoffeeMachineInventory : Inventory
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
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInventoryHandler))]
public class PlayerInventory : Inventory
{
    //item manager is null at start
    //TODO: fina out a reason behind this, possible because of using multiple installers
    private void Start()
    {
       StartCoroutine(WaitBug());
    }

    private IEnumerator WaitBug()
    {
        yield return new WaitForEndOfFrame();
        var coffee = ItemManager.GetItem(InventoryItemType.Coffee);
        print(coffee);
        AllowedItems = new(1)
        {
            { coffee.ItemType(), coffee }
        };
    }
}
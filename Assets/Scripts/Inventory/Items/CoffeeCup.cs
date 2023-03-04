using Pickupable;
using UnityEngine;

public class CoffeeCup : MonoBehaviour, IPickupable
{
    private CoffeeItem _item = new();

    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(_item))
        {
            inventory.ReturnToPool(_item, gameObject);
        }
    }
}
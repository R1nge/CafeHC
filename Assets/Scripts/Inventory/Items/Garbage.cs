using Pickupable;
using UnityEngine;

public class Garbage : MonoBehaviour, IPickupable
{
    private GarbageItem _item = new();
    
    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(_item))
        {
            inventory.ReturnToPool(_item, gameObject);
        }
    }
}
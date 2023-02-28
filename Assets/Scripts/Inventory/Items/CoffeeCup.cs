using Pickupable;
using Trashable;
using UnityEngine;
using Zenject;

public class CoffeeCup : MonoBehaviour, IPickupable, ITrashable
{
    [SerializeField] private InventoryItem item;
    private Inventory _inventory;

    [Inject]
    public void Construct(Inventory inventory) => _inventory = inventory;

    public void Pickup()
    {
        if (_inventory.TryAddItem(item))
        {
            Destroy(gameObject);
        }
    }

    public void Trash()
    {
        _inventory.RemoveItem(item);
        Destroy(gameObject);
    }
}
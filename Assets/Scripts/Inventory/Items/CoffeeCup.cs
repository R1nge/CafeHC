using Pickupable;
using Trashable;
using UnityEngine;
using Zenject;

public class CoffeeCup : MonoBehaviour, IPickupable, ITrashable
{
    [SerializeField] private InventoryItem item;
    private Inventory _inventory;
    private CoffeeFactory _coffeeFactory;

    [Inject]
    public void Construct(Inventory inventory, CoffeeFactory coffeeFactory)
    {
        _inventory = inventory;
        _coffeeFactory = coffeeFactory;
    }

    public void Pickup()
    {
        if (_inventory.TryAddItem(item))
        {
            _coffeeFactory.ReturnToPool(gameObject);
        }
    }

    public void Trash()
    {
        _inventory.RemoveItem(item);
        _coffeeFactory.ReturnToPool(gameObject);
    }
}
using Pickupable;
using UnityEngine;
using Zenject;

public class CoffeeCup : MonoBehaviour, IPickupable
{
    [SerializeField] private InventoryItem item;
    private CoffeeFactory _coffeeFactory;

    [Inject]
    public void Construct(CoffeeFactory coffeeFactory)
    {
        _coffeeFactory = coffeeFactory;
    }

    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(item))
        {
            _coffeeFactory.ReturnToPool(gameObject);
        }
    }
}
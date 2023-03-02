using Pickupable;
using UnityEngine;
using Zenject;

public class CoffeeCup : MonoBehaviour, IPickupable
{
    [SerializeField] private InventoryItem item;
    private CoffeeFactory _coffeeFactory;
    
    [Inject]
    private void Construct(CoffeeFactory factory) => _coffeeFactory = factory;

    public void Pickup(Inventory inventory)
    {
        if (inventory.TryAddItem(item))
        {
            _coffeeFactory.ReturnToPool(gameObject);
        }
    }
}
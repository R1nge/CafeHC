using Pickupable;
using Trashable;
using UnityEngine;
using Zenject;

public class CoffeeCup : MonoBehaviour, IPickupable, ITrashable
{
    [SerializeField] private InventoryItem inventoryItem = new();
    private Inventory _inventory;

    [Inject]
    public void Construct(Inventory inventory) => _inventory = inventory;

    public void Pickup()
    {
        _inventory.AddItem(inventoryItem);
        Destroy(gameObject);
    }

    public void Trash()
    {
        _inventory.RemoveItem(inventoryItem);
        Destroy(gameObject);
    }
}
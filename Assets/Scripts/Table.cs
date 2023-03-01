using UnityEngine;
using Zenject;

public class Table : MonoBehaviour
{
    private Inventory _inventory;
    private CoffeeFactory _coffeeFactory;
    private int _currentCount;

    [Inject]
    public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        _inventory.OnItemAddedEvent += OnItemAdded;
        _inventory.OnItemRemovedEvent += OnItemRemoved;
    }

    private void OnItemAdded(InventoryItem item)
    {
        var pos = new Vector3(
            transform.position.x,
            transform.position.y + 0.1f + 0.05f * _currentCount,
            transform.position.z
        );

        _coffeeFactory.GetFromPool(pos, Quaternion.identity, transform);
        _currentCount++;
    }

    private void OnAllItemsRemoved()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _coffeeFactory.ReturnToPool(transform.GetChild(i).gameObject);
        }

        _currentCount = 0;
    }

    private void OnItemRemoved(InventoryItem item)
    {
        _coffeeFactory.ReturnToPool(transform.GetChild(transform.childCount - 1).gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CustomerInventory customerInventory))
        {
            customerInventory.TryTransferTo(_inventory);
        }
    }

    private void OnDestroy()
    {
        _inventory.OnAllItemsRemovedEvent -= OnAllItemsRemoved;
        _inventory.OnItemAddedEvent -= OnItemAdded;
        _inventory.OnItemRemovedEvent -= OnItemRemoved;
    }
}
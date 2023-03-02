using UnityEngine;
using Zenject;

public class Counter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private CoffeeFactory _coffeeFactory;
    private int _currentCount;
    private Inventory _inventory;

    [Inject]
    private void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnItemAddedEvent += Spawn;
        _inventory.OnItemRemovedEvent += DeSpawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            Add(playerInventory);
        }
        else if (other.TryGetComponent(out CustomerInventory customerInventory))
        {
            TransferTo(customerInventory);
        }
    }

    private void Add(PlayerInventory playerInventory)
    {
        var count = playerInventory.GetCount();
        for (int i = 0; i < count; i++)
        {
            playerInventory.TryTransferTo(_inventory);
        }
    }

    private void TransferTo(CustomerInventory customerInventory)
    {
        for (int i = 0; i < _currentCount; i++)
        {
            _inventory.TryTransferTo(customerInventory);
        }

        _currentCount = 0;
    }

    private void Spawn(InventoryItem item)
    {
        var pos = new Vector3(
            spawnPoint.position.x,
            spawnPoint.position.y + 0.05f * _currentCount,
            spawnPoint.position.z
        );

        _coffeeFactory.GetFromPool(pos, Quaternion.identity, spawnPoint);
        _currentCount++;
    }

    private void DeSpawn(InventoryItem item)
    {
        _coffeeFactory.ReturnToPool(spawnPoint.GetChild(spawnPoint.childCount - 1).gameObject);
    }

    private void OnDestroy()
    {
        _inventory.OnItemAddedEvent -= Spawn;
        _inventory.OnItemRemovedEvent -= DeSpawn;
    }
}
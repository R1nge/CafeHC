using UnityEngine;
using Zenject;

public class Counter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private CoffeeFactory _coffeeFactory;
    private int _currentCount;
    private Inventory _inventory;

    [Inject]
    public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

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
            Give(customerInventory);
        }
    }

    private void Add(PlayerInventory playerInventory)
    {
        for (int i = 0; i < 5; i++)
        {
            playerInventory.TryTransferTo(_inventory);
        }
    }

    private void Give(CustomerInventory customerInventory)
    {
        for (int i = 0; i < _currentCount; i++)
        {
            _inventory.TryTransferTo(customerInventory);
        }
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
        _currentCount--;
    }

    private void OnDestroy()
    {
        _inventory.OnItemAddedEvent -= Spawn;
        _inventory.OnItemRemovedEvent -= DeSpawn;
    }
}
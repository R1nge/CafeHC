using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float offsetY;
    private int _currentCount;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnItemAddedEvent += Spawn;
        _inventory.OnItemRemovedEvent += DeSpawn;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            TransferFrom(playerInventory);
        }
        else if (other.TryGetComponent(out CustomerInventory customerInventory))
        {
            TransferTo(customerInventory);
        }
    }

    private void TransferFrom(PlayerInventory playerInventory)
    {
        if (playerInventory.GetCount() == 0) return;

        if (playerInventory.GetItem().CompareType(InventoryItem.ItemType.Coffee))
        {
            var count = playerInventory.GetCount();
            for (int i = 0; i < count; i++)
            {
                playerInventory.TryTransferTo(_inventory);
            }
        }
    }

    private void TransferTo(CustomerInventory customerInventory)
    {
        var count = _inventory.GetCount();
        for (int i = 0; i < count; i++)
        {
            _inventory.TryTransferTo(customerInventory);
        }

        _currentCount = _inventory.GetCount();
    }

    private void Spawn(InventoryItem item)
    {
        var pos = new Vector3(
            spawnPoint.position.x,
            spawnPoint.position.y + offsetY * _currentCount,
            spawnPoint.position.z
        );

        _inventory.GetFromPool(item, pos, Quaternion.identity, spawnPoint);
        _currentCount++;
    }

    private void DeSpawn(InventoryItem item)
    {
        _inventory.ReturnToPool(item, spawnPoint.GetChild(spawnPoint.childCount - 1).gameObject);
        _currentCount--;
    }

    private void OnDestroy()
    {
        _inventory.OnItemAddedEvent -= Spawn;
        _inventory.OnItemRemovedEvent -= DeSpawn;
    }
}
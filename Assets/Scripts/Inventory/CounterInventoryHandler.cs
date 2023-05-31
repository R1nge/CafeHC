using UnityEngine;

public class CounterInventoryHandler : InventoryHandler
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float offsetY;

    protected override void OnItemAdded(InventoryItem item)
    {
        item.GetFromPool(SpawnPosition(), Quaternion.identity, spawnPoint);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        item.ReturnToPool(spawnPoint.GetChild(spawnPoint.childCount - 1).gameObject);
    }

    private Vector3 SpawnPosition()
    {
        return new(
            spawnPoint.position.x,
            spawnPoint.position.y + offsetY * Inventory.GetCount(),
            spawnPoint.position.z
        );
    }
}
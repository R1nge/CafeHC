using UnityEngine;

public class CoffeeMachineInventoryHandler : InventoryHandler
{
    [SerializeField] private float offsetX, offsetY;
    [SerializeField] private Transform spawnPoint;

    protected override void OnItemAdded(InventoryItem item)
    {
        item.GetFromPool(GetPosition(), Quaternion.identity, spawnPoint);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        item.ReturnToPool(spawnPoint.GetChild(spawnPoint.childCount - 1).gameObject);
    }

    private Vector3 GetPosition()
    {
        var position = spawnPoint.position - Vector3.right * offsetX / 2f;
        var count = Inventory.GetCount();

        if (count % 2 == 0)
        {
            position += Vector3.right * offsetX;
            position += new Vector3(0, offsetY * Mathf.Floor((count - 1) / 2f));
            return position;
        }

        if (count % 2 == 1)
        {
            position += new Vector3(0, offsetY * Mathf.Floor(count / 2f));
        }

        return position;
    }
}
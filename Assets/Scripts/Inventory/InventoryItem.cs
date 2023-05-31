using UnityEngine;

public abstract class InventoryItem
{
    public abstract InventoryItemType ItemType();
    public abstract bool IgnoreCapacity();
    public abstract void GetFromPool(Vector3 position, Quaternion rotation, Transform parent);
    public abstract void ReturnToPool(GameObject go);
}
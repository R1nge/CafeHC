using UnityEngine;

public class GarbageItem : InventoryItem
{
    private readonly GarbageFactory _garbageFactory;
    
    public GarbageItem(GarbageFactory garbageFactory)
    {
        _garbageFactory = garbageFactory;
    }

    public override InventoryItemType ItemType() => InventoryItemType.Garbage;

    public override bool IgnoreCapacity() => true;

    public override void GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
        _garbageFactory.GetFromPool(position, rotation, parent);
    }

    public override void ReturnToPool(GameObject gameObject)
    {
        _garbageFactory.ReturnToPool(gameObject);
    }
}
using UnityEngine;

public class CoffeeItem : InventoryItem
{
    private readonly CoffeeFactory _coffeeFactory;

    public CoffeeItem(CoffeeFactory coffeeFactory)
    {
        _coffeeFactory = coffeeFactory;
    }

    public override InventoryItemType ItemType() => InventoryItemType.Coffee;

    public override bool IgnoreCapacity() => false;

    public override void GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
        _coffeeFactory.GetFromPool(position, rotation, parent);
    }

    public override void ReturnToPool(GameObject gameObject)
    {
        _coffeeFactory.ReturnToPool(gameObject);
    }
}
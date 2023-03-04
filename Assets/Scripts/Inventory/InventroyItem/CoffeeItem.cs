public class CoffeeItem : InventoryItem
{
    public CoffeeItem() => itemType = ItemType.Coffee;

    public override ItemType itemType { get; }
}
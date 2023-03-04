public class GarbageItem : InventoryItem
{
    public GarbageItem() => itemType = ItemType.Garbage;

    public override ItemType itemType { get; }
}
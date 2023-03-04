public abstract class InventoryItem
{
    public enum ItemType
    {
        Coffee,
        Garbage
    }

    public abstract ItemType itemType { get; }

    public bool CompareType(ItemType type) => itemType.Equals(type);
}
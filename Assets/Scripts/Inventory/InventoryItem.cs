public abstract class InventoryItem
{
    public enum ItemType
    {
        Coffee,
        Garbage
    }

    public ItemType itemType;

    public bool CompareType(ItemType type) => itemType.Equals(type);
}
using UnityEngine;

public class PlayerInventoryHandler : InventoryHandler
{
    [SerializeField] private Transform hand;
    [SerializeField] private float offsetY;

    protected override void OnItemAdded(InventoryItem item)
    {
        item.GetFromPool(GetPosition(), Quaternion.identity, hand);
    }

    protected override void OnItemRemoved(InventoryItem item)
    {
        item.ReturnToPool(hand.GetChild(hand.childCount - 1).gameObject);
    }

    protected override void OnAllItemsRemoved(InventoryItem item)
    {
        for (int i = hand.childCount - 1; i >= 0; i--)
        {
            item.ReturnToPool(hand.GetChild(i).gameObject);
        }
    }

    private Vector3 GetPosition()
    {
        return hand.position + new Vector3(0, offsetY * (Inventory.GetCount() - 1), 0);
    }
}
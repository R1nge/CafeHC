using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private GameObject itemModel;

    public ItemType GetItemType() => itemType;
    public virtual GameObject GetItemModel() => itemModel;

    public enum ItemType
    {
        CoffeeCup,
        Garbage
    }
}
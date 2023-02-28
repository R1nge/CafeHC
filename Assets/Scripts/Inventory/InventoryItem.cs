using System;
using UnityEngine;

[Serializable]
public struct InventoryItem
{
    [SerializeField] private string itemName;

    public string GetItemName() => itemName;
}
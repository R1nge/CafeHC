using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject model;

    public string GetItemName() => itemName;

    public GameObject GetItemModel() => model;
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private readonly List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;

    public void AddItem(InventoryItem item)
    {
        _items.Add(item);
        OnItemAddedEvent?.Invoke(item);
        LogItems();
    }

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        OnItemRemovedEvent?.Invoke(item);
        LogItems();
    }

    public void RemoveAllItems()
    {
        _items.Clear();
        OnAllItemsRemovedEvent?.Invoke();
        LogItems();
    }

    private void LogItems()
    {
        if (_items.Count == 0)
        {
            Debug.Log("No Items");
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            Debug.Log(_items[i].GetItemName());
        }
    }
}
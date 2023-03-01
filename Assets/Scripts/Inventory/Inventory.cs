using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int _maxAmount = 5;
    private int _currentAmount;
    private List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;
    public event Action<int> OnMaxAmountChangedEvent;

    public bool TryAddItem(InventoryItem item)
    {
        if (CanAddItem())
        {
            _items.Add(item);
            _currentAmount = _items.Count;
            OnItemAddedEvent?.Invoke(item);
            LogItems();
            return true;
        }

        return false;
    }

    private bool CanAddItem() => _currentAmount < _maxAmount;

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        _currentAmount = _items.Count;
        OnItemRemovedEvent?.Invoke(item);
        LogItems();
    }

    public void RemoveAllItems()
    {
        _currentAmount = 0;
        _items.Clear();
        OnAllItemsRemovedEvent?.Invoke();
    }

    public void SetMaxAmount(int maxAmount)
    {
        _maxAmount = maxAmount;
        OnMaxAmountChangedEvent?.Invoke(_maxAmount);
    }

    private void LogItems()
    {
        Debug.Log("/////////////////////");
        for (int i = 0; i < _items.Count; i++)
        {
            Debug.Log(_items[i].GetItemName());
        }
    }
}
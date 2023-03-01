using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] private int maxAmount = 5;
    private int _currentAmount;
    private readonly List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;
    public event Action<int> OnMaxAmountChangedEvent;

    public InventoryItem TakeItem()
    {
        if (_items.Count == 0) return null;
        var _item = _items[^1];
        RemoveItem(_items[^1]);
        return _item;
    }

    public bool TryTransferTo(Inventory inventory)
    {
        var item = TakeItem();
        if (inventory.CanAddItem(item))
        {
            inventory.TryAddItem(item);
            return true;
        }

        return false;
    }

    public bool TryAddItem(InventoryItem item)
    {
        if (CanAddItem(item))
        {
            _items.Add(item);
            _currentAmount = _items.Count;
            OnItemAddedEvent?.Invoke(item);
            return true;
        }

        return false;
    }

    private bool CanAddItem(InventoryItem item)
    {
        if (item == null) return false;
        return _currentAmount < maxAmount;
    }

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        _currentAmount = _items.Count;
        OnItemRemovedEvent?.Invoke(item);
    }

    public void RemoveAllItems()
    {
        _currentAmount = 0;
        _items.Clear();
        OnAllItemsRemovedEvent?.Invoke();
    }

    public void SetMaxAmount(int maxAmount)
    {
        this.maxAmount = maxAmount;
        OnMaxAmountChangedEvent?.Invoke(this.maxAmount);
    }
}
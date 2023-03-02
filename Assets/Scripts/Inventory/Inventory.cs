using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] private int maxAmount = 5;
    private readonly List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;
    public event Action<int> OnMaxAmountChangedEvent;

    public int GetCount() => _items.Count;

    public InventoryItem GetItem()
    {
        if (_items.Count == 0) return null;
        return _items[^1];
    }

    public bool TryTransferTo(Inventory inventory)
    {
        var item = GetItem();
        if (inventory.CanAddItem(item))
        {
            if (inventory.TryAddItem(item))
            {
                RemoveItem(_items[^1]);
            }

            return true;
        }

        return false;
    }

    public bool TryAddItem(InventoryItem item)
    {
        if (CanAddItem(item))
        {
            _items.Add(item);
            OnItemAddedEvent?.Invoke(item);
            return true;
        }

        return false;
    }

    private bool CanAddItem(InventoryItem item)
    {
        if (item == null) return false;
        if (_items.Count != 0)
        {
            if (_items[^1].GetItemType() != item.GetItemType()) return false;
        }

        if (item.GetItemType() == InventoryItem.ItemType.Garbage) return true;
        return _items.Count < maxAmount;
    }

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        OnItemRemovedEvent?.Invoke(item);
    }

    public void RemoveAllItems()
    {
        _items.Clear();
        OnAllItemsRemovedEvent?.Invoke();
    }

    public void SetMaxAmount(int maxAmount)
    {
        this.maxAmount = maxAmount;
        OnMaxAmountChangedEvent?.Invoke(this.maxAmount);
    }
}
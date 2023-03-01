using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField] private int maxAmount = 5;
    private int _currentAmount;
    private List<InventoryItem> _items = new();

    //for correct Zenject binding
    public Inventory() { }

    public Inventory(int maxAmount) => this.maxAmount = maxAmount;

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;
    public event Action<int> OnMaxAmountChangedEvent;

    public InventoryItem TakeItem(InventoryItem item)
    {
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            if (_items[i].GetItemName() == item.GetItemName())
            {
                var _item = _items[i];
                RemoveItem(_items[i]);
                return _item;
            }
        }

        return null;
    }

    public bool TryTransferTo(InventoryItem item, Inventory inventory)
    {
        if (item == null) return false;
        if (inventory.TryAddItem(item))
        {
            return true;
        }

        return false;
    }

    public bool TryAddItem(InventoryItem item)
    {
        if (CanAddItem())
        {
            _items.Add(item);
            _currentAmount = _items.Count;
            OnItemAddedEvent?.Invoke(item);
            return true;
        }

        return false;
    }

    private bool CanAddItem() => _currentAmount < maxAmount;

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
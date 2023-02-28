using System;
using System.Collections.Generic;

public class Inventory
{
    //TODO: make customizable, maybe using public method or scriptable object
    private int _maxAmount = 5;
    private int _currentAmount;
    private List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action OnAllItemsRemovedEvent;

    public bool TryAddItem(InventoryItem item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].GetItemName();
        }
        
        if (CanAddItem())
        {
            _items.Add(item);
            _currentAmount = _items.Count;
            OnItemAddedEvent?.Invoke(item);
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
    }

    public void RemoveAllItems()
    {
        _currentAmount = 0;
        _items.Clear();
        OnAllItemsRemovedEvent?.Invoke();
    }
}
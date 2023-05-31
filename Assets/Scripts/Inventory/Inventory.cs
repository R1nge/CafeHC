using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] private int size = 5;
    protected Dictionary<InventoryItemType, InventoryItem> AllowedItems = new();
    protected ItemManager ItemManager;
    private readonly List<InventoryItem> _items = new();

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action<InventoryItem> OnAllItemsRemovedEvent;
    public event Action<int> OnSizeChangedEvent;

    [Inject]
    protected void Inject(ItemManager itemManager)
    {
        ItemManager = itemManager;
    }

    public int GetMaxAmount() => size;

    public int GetCount() => _items.Count;

    public bool IsFull() => _items.Count == size;

    public InventoryItem GetItem()
    {
        if (_items.Count == 0) return null;
        return _items[^1];
    }

    public void TryTransferTo(Inventory inventory)
    {
        if (inventory.TryAddItem(GetItem()))
        {
            RemoveItem(_items[^1]);
        }
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
        if (item == null)
        {
            return false;
        }

        if (AllowedItems.ContainsKey(item.ItemType()))
        {
            if (_items.Count < size)
            {
                if (_items.Count != 0)
                {
                    return _items[^1].ItemType() == item.ItemType();
                }

                return true;
            }
        }

        return IgnoreCapacity(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        OnItemRemovedEvent?.Invoke(item);
    }

    public void RemoveAllItems()
    {
        if (GetCount() == 0) return;
        OnAllItemsRemovedEvent?.Invoke(_items[^1]);
        _items.Clear();
    }

    public void SetSize(int size)
    {
        this.size = size;
        OnSizeChangedEvent?.Invoke(this.size);
    }

    private bool IgnoreCapacity(InventoryItem item)
    {
        return item.IgnoreCapacity();
    }
}
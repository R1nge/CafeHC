using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] private int maxAmount = 5;
    private readonly List<InventoryItem> _items = new();
    private CoffeeFactory _coffeeFactory;
    private GarbageFactory _garbageFactory;

    public event Action<InventoryItem> OnItemAddedEvent;
    public event Action<InventoryItem> OnItemRemovedEvent;
    public event Action<InventoryItem> OnAllItemsRemovedEvent;
    public event Action<int> OnMaxAmountChangedEvent;

    [Inject]
    private void Construct(CoffeeFactory coffeeFactory, GarbageFactory garbageFactory)
    {
        _coffeeFactory = coffeeFactory;
        _garbageFactory = garbageFactory;
    }

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
        if (item == null)
        {
            return false;
        }
        
        if (_items.Count != 0)
        {
            if (_items.Count < maxAmount)
            {
                return _items[^1].GetType() == item.GetType();
            }

            if (item.CompareType(InventoryItem.ItemType.Garbage))
            {
                return true;
            }

            return false;
        }
        
        if (_items.Count < maxAmount)
        {
            return true;
        }

        if (item.CompareType(InventoryItem.ItemType.Garbage))
        {
            return true;
        }
        
        return false;
    }

    public void RemoveItem(InventoryItem item)
    {
        _items.Remove(item);
        OnItemRemovedEvent?.Invoke(item);
    }

    public void RemoveAllItems()
    {
        OnAllItemsRemovedEvent?.Invoke(_items[^1]);
        _items.Clear();
    }

    public void SetMaxAmount(int maxAmount)
    {
        this.maxAmount = maxAmount;
        OnMaxAmountChangedEvent?.Invoke(this.maxAmount);
    }

    public void GetFromPool(InventoryItem item, Vector3 position, Quaternion rotation, Transform parent)
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Coffee:
                _coffeeFactory.GetFromPool(position, rotation, parent);
                break;
            case InventoryItem.ItemType.Garbage:
                _garbageFactory.GetFromPool(position, rotation, parent);
                break;
        }
    }

    public void ReturnToPool(InventoryItem item, GameObject go)
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.Coffee:
                _coffeeFactory.ReturnToPool(go);
                break;
            case InventoryItem.ItemType.Garbage:
                _garbageFactory.ReturnToPool(go);
                break;
        }
    }
}
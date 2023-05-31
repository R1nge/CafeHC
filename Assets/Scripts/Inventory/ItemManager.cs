using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemManager : IInitializable
{
    private Dictionary<InventoryItemType, InventoryItem> _items;
    private readonly CoffeeFactory _coffeeFactory;
    private readonly GarbageFactory _garbageFactory;

    [Inject]
    public ItemManager(CoffeeFactory coffeeFactory, GarbageFactory garbageFactory)
    {
        _coffeeFactory = coffeeFactory;
        _garbageFactory = garbageFactory;
    }

    public void Initialize()
    {
        var coffee = new CoffeeItem(_coffeeFactory);
        var garbage = new GarbageItem(_garbageFactory);

        _items = new()
        {
            { InventoryItemType.Coffee, coffee },
            { InventoryItemType.Garbage, garbage }
        };
    }

    public InventoryItem GetItem(InventoryItemType type) => _items[type];
}

public enum InventoryItemType : byte
{
    Coffee,
    Garbage
}
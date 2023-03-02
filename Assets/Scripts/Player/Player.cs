using System;
using Pickupable;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        private Inventory _inventory;
        private CoffeeFactory _coffeeFactory;
        private GarbageFactory _garbageFactory;

        [Inject]
        private void Construct(CoffeeFactory coffeeFactory, GarbageFactory garbageFactory)
        {
            _coffeeFactory = coffeeFactory;
            _garbageFactory = garbageFactory;
        }

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            //TODO: think of a better solution
            switch (item.GetItemType())
            {
                case InventoryItem.ItemType.CoffeeCup:
                    _coffeeFactory.GetFromPool(hand.position, Quaternion.identity, hand);
                    break;
                case InventoryItem.ItemType.Garbage:
                    _garbageFactory.GetFromPool(hand.position, Quaternion.identity, hand);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnItemRemoved(InventoryItem item)
        {
            switch (item.GetItemType())
            {
                case InventoryItem.ItemType.CoffeeCup:
                    _coffeeFactory.ReturnToPool(hand.GetChild(hand.childCount - 1).gameObject);
                    break;
                case InventoryItem.ItemType.Garbage:
                    _garbageFactory.ReturnToPool(hand.GetChild(hand.childCount - 1).gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnAllItemsRemoved()
        {
            for (int i = hand.childCount - 1; i >= 0; i--)
            {
                _coffeeFactory.ReturnToPool(hand.GetChild(i).gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickupable pickupable))
            {
                pickupable.Pickup(_inventory);
            }
        }

        private void OnDestroy()
        {
            _inventory.OnItemAddedEvent -= OnItemAdded;
            _inventory.OnItemRemovedEvent -= OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent -= OnAllItemsRemoved;
        }
    }
}
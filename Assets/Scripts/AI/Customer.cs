using System;
using UnityEngine;
using Zenject;

namespace AI
{
    public class Customer : MonoBehaviour
    {
        private Inventory _inventory;
        private CoffeeFactory _coffeeFactory;

        [Inject]
        public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            _coffeeFactory.GetFromPool(transform.position, Quaternion.identity, transform);
        }

        private void OnItemRemoved(InventoryItem item)
        {
            _coffeeFactory.ReturnToPool(transform.GetChild(transform.childCount - 1).gameObject);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAddedEvent -= OnItemAdded;
            _inventory.OnItemRemovedEvent -= OnItemRemoved;
        }
    }
}
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
        private Spawner _spawner;

        [Inject]
        public void Construct(Inventory inventory) => _inventory = inventory;

        //BUG: NEVER SUBSCRIBE IN "CONSTRUCTOR", LOST 5 HOURS BECAUSE OF IT
        private void Start()
        {
            _spawner = FindObjectOfType<Spawner>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        }

        private void OnItemAdded(InventoryItem item) => _spawner.Spawn(hand);

        private void OnItemRemoved(InventoryItem item) => Destroy(hand.GetChild(hand.childCount - 1).gameObject);

        private void OnAllItemsRemoved()
        {
            for (int i = hand.childCount - 1; i >= 0; i--)
            {
                Destroy(hand.GetChild(i).gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickupable pickupable))
            {
                pickupable.Pickup();
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
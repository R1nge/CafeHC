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

        [Inject]
        public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;
        
        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            _coffeeFactory.GetFromPool(
                hand.position,
                Quaternion.identity,
                hand
            );
        }

        private void OnItemRemoved(InventoryItem item)
        {
            _coffeeFactory.ReturnToPool(hand.GetChild(hand.childCount - 1).gameObject);
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
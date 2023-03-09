using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private float offsetY;
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            _inventory.GetFromPool(item, GetPosition(), Quaternion.identity, hand);
        }

        private Vector3 GetPosition()
        {
            return hand.position + new Vector3(0, offsetY * (_inventory.GetCount() - 1), 0);
        }

        private void OnItemRemoved(InventoryItem item)
        {
            var lastChild = hand.GetChild(hand.childCount - 1).gameObject;
            _inventory.ReturnToPool(item, lastChild);
        }

        private void OnAllItemsRemoved(InventoryItem item)
        {
            for (int i = hand.childCount - 1; i >= 0; i--)
            {
                var child = hand.GetChild(i).gameObject;
                _inventory.ReturnToPool(item, child);
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
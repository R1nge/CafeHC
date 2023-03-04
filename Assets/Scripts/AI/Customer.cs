using UnityEngine;

namespace AI
{
    public class Customer : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            _inventory.GetFromPool(item, transform.position, Quaternion.identity, transform);
        }

        private void OnItemRemoved(InventoryItem item)
        {
            _inventory.ReturnToPool(item, transform.GetChild(transform.childCount - 1).gameObject);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAddedEvent -= OnItemAdded;
            _inventory.OnItemRemovedEvent -= OnItemRemoved;
        }
    }
}
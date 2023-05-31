using System;
using TMPro;
using UnityEngine;

namespace AI
{
    public class CustomerInventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI orderAmount;
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
        }

        private void OnItemAdded(InventoryItem item) => UpdateUI();

        public void UpdateUI()
        {
            var amount = _inventory.GetMaxAmount() - _inventory.GetCount();
            orderAmount.text = amount <= 0 ? String.Empty : amount.ToString();
        }

        private void OnDestroy() => _inventory.OnItemAddedEvent -= OnItemAdded;
    }
}
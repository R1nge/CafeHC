using System;
using TMPro;
using UnityEngine;

namespace AI
{
    public class CustomerInventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI orderAmount;

        public void UpdateUI(int amount)
        {
            orderAmount.text = amount <= 0 ? String.Empty : amount.ToString();
        }
    }
}
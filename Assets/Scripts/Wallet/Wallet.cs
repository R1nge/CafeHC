using System;
using UnityEngine;

namespace Wallet
{
    public sealed class Wallet
    {
        private int _money;
        private const string MoneyString = "Money";

        private int Money
        {
            get => _money;
            set
            {
                _money = value;
                OnMoneyAmountChanged?.Invoke(_money);
            }
        }

        public event Action<int> OnMoneyAmountChanged;

        public void Earn(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Trying to earn negative value");
                return;
            }

            Money += amount;
        }

        public bool Spend(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Trying to spend negative value");
                return false;
            }

            if (Money - amount >= 0)
            {
                Money -= amount;
                return true;
            }

            return false;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(nameof(MoneyString), Money);
            PlayerPrefs.Save();
        }

        public void Load() => Money = PlayerPrefs.GetInt(nameof(MoneyString), 0);
    }
}
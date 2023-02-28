using System;
using UnityEngine;

public class Wallet : MonoBehaviour
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

    private void Start() => Load();

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

        if (Money - amount > 0)
        {
            Money -= amount;
            return true;
        }

        return false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(nameof(MoneyString), Money);
        PlayerPrefs.Save();
    }

    private void Load() => Money = PlayerPrefs.GetInt(nameof(MoneyString), 0);

    private void OnDestroy() => Save();
}
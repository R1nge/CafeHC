using System.Collections;
using UnityEngine;
using Zenject;

public class MoneyArea : MonoBehaviour
{
    [SerializeField] private int moneyPerBanknote;
    [SerializeField] private GameObject[] banknotes;
    private int _currentShownAmount;
    private Wallet.Wallet _wallet;

    [Inject]
    private void Construct(Wallet.Wallet wallet) => _wallet = wallet;

    public void AddMoney(int amount)
    {
        _currentShownAmount += amount;
        Show();
    }

    private void Awake()
    {
        _currentShownAmount = banknotes.Length;

        for (int i = _currentShownAmount - 1; i >= 0; i--)
        {
            Hide(i);
        }
    }

    private void Show()
    {
        for (int i = 0; i < _currentShownAmount; i++)
        {
            if (banknotes[i].activeInHierarchy) continue;
            banknotes[i].SetActive(true);
        }
    }

    private void Hide(int index)
    {
        banknotes[index].SetActive(false);
        _currentShownAmount--;
    }

    private void Earn() => _wallet.Earn(moneyPerBanknote);

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (_currentShownAmount == 0) yield break;
        if (other.TryGetComponent(out Player.Player _))
        {
            while (_currentShownAmount > 0)
            {
                Earn();
                Hide(_currentShownAmount - 1);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player.Player _))
        {
            StopAllCoroutines();
        }
    }
}
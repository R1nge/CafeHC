using System.Collections;
using UnityEngine;
using Zenject;

public class MoneyZone : MonoBehaviour
{
    [SerializeField] private int moneyPerBanknote;
    [SerializeField] private GameObject[] banknotes;
    private int _currentShownAmount;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet) => _wallet = wallet;

    private void Start()
    {
        _currentShownAmount = banknotes.Length;
        Show();
    }

    private void Show()
    {
        for (int i = 0; i < _currentShownAmount; i++)
        {
            if (banknotes[i].activeInHierarchy) continue;
            banknotes[i].SetActive(true);
        }
    }

    private void Hide(int index) => banknotes[index].SetActive(false);

    private void Earn() => _wallet.Earn(moneyPerBanknote);

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (_currentShownAmount == 0) yield break;
        if (other.TryGetComponent(out Player.Player _))
        {
            for (int i = _currentShownAmount - 1; i >= 0; i--)
            {
                Earn();
                Hide(i);
                yield return new WaitForSeconds(0.01f);
                _currentShownAmount--;
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
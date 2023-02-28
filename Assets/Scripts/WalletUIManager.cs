using TMPro;
using UnityEngine;
using Zenject;

public class WalletUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private Wallet _wallet;

    [Inject]
    public void Construct(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.OnMoneyAmountChanged += UpdateUI;
    }

    private void UpdateUI(int value) => moneyText.text = value.ToString();

    private void OnDestroy() => _wallet.OnMoneyAmountChanged -= UpdateUI;
}
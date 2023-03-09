using TMPro;
using UnityEngine;

public class UnlockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI price;

    //TODO: use ZString
    public void UpdateUI(int value)
    {
        price.text = value.ToString();
    }
}
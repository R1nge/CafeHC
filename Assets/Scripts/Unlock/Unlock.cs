using System.Collections;
using UnityEngine;
using Zenject;

public class Unlock : MonoBehaviour
{
    [SerializeField] private int[] prices;
    [SerializeField] private GameObject[] gameObjects;
    private int _currentLevel;
    private int _moneyLeftToTheNextLevel;
    private Wallet _wallet;
    private UnlockUI _unlockUI;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void Awake()
    {
        _unlockUI = GetComponent<UnlockUI>();
        _moneyLeftToTheNextLevel = prices[_currentLevel];
        _unlockUI.UpdateUI(_moneyLeftToTheNextLevel);
        //TODO: save and load
    }

    private void TryUnlock()
    {
        if (_moneyLeftToTheNextLevel == 0)
        {
            if (_currentLevel == 0)
            {
                gameObjects[_currentLevel].SetActive(true);
                TryIncreaseLevel();
                if (!TryIncreaseLevel())
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObjects[_currentLevel].SetActive(false);
                if (TryIncreaseLevel())
                {
                    gameObjects[_currentLevel].SetActive(true);
                    _moneyLeftToTheNextLevel = prices[_currentLevel];
                    _unlockUI.UpdateUI(_moneyLeftToTheNextLevel);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private bool TryIncreaseLevel()
    {
        if (_currentLevel == gameObjects.Length)
        {
            return false;
        }

        _currentLevel++;
        return true;
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        while (enabled)
        {
            if (_currentLevel == gameObjects.Length) yield break;
            if (!other.TryGetComponent(out Player.Player player)) yield break;
            if (_wallet.Spend(1))
            {
                _moneyLeftToTheNextLevel--;
                _unlockUI.UpdateUI(_moneyLeftToTheNextLevel);
                TryUnlock();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player.Player player))
        {
            StopAllCoroutines();
        }
    }
}
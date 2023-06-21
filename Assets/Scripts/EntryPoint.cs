using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    private Wallet.Wallet _wallet;
    private ItemManager _itemManager;
    private Player.Player _player;

    [Inject]
    private void Construct(Wallet.Wallet wallet, ItemManager itemManager, Player.Player player)
    {
        _wallet = wallet;
        _itemManager = itemManager;
        _player = player;
    }

    private void Start()
    {
        _wallet.Load();
        _itemManager.Initialize();
        _player.GetComponent<PlayerInventory>().Initialize();
    }

    private void OnDestroy()
    {
        _wallet.Save();
    }
}
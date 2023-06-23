using System;
using Zenject;

public class EntryPoint : IInitializable, IDisposable
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

    public void Initialize()
    {
        _wallet.Load();
        _itemManager.Initialize();
        _player.GetComponent<PlayerInventory>().Initialize();
    }

    public void Dispose()
    {
        _wallet.Save();
    }
}
using UnityEngine;
using Zenject;

namespace DI
{
    public class WalletInstaller : MonoInstaller
    {
        [SerializeField] private Wallet wallet;
        
        public override void InstallBindings()
        {
            var walletInstance = Container.InstantiatePrefabForComponent<Wallet>(wallet);
            Container.Bind<Wallet>().FromInstance(walletInstance).AsSingle();
            Container.QueueForInject(wallet);
        }
    }
}
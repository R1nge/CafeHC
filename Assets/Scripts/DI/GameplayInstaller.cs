using UnityEngine;
using Zenject;

namespace DI
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CoffeeFactory coffeeFactory;
        [SerializeField] private GarbageFactory garbageFactory;
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private Transform joystickParent;
        [SerializeField] private Player.Player player;
        [SerializeField] private Transform spawnPoint;

        public override void InstallBindings()
        {
            BindCoffeeFactory();
            BindGarbageFactory();
            BindItemManager();
            BindJoystick();
            BindWallet();
            BindPlayer();
            BindEntryPoint();
        }

        private void BindCoffeeFactory()
        {
            var coffeeFactoryInstance = Container.InstantiatePrefabForComponent<CoffeeFactory>(coffeeFactory);
            Container.Bind<CoffeeFactory>().FromInstance(coffeeFactoryInstance).AsSingle();
            Container.QueueForInject(coffeeFactoryInstance);
        }

        private void BindGarbageFactory()
        {
            var garbageFactoryInstance = Container.InstantiatePrefabForComponent<GarbageFactory>(garbageFactory);
            Container.Bind<GarbageFactory>().FromInstance(garbageFactoryInstance).AsSingle();
            Container.QueueForInject(garbageFactoryInstance);
        }

        private void BindItemManager()
        {
            Container.BindInterfacesAndSelfTo<ItemManager>().AsSingle().NonLazy();
        }

        private void BindJoystick()
        {
            var joystickInstance = Container.InstantiatePrefabForComponent<FloatingJoystick>(floatingJoystick);
            JoystickSetup(joystickInstance);
            Container.Bind<FloatingJoystick>().FromInstance(joystickInstance).AsSingle();
            Container.QueueForInject(joystickInstance);
        }

        private void JoystickSetup(FloatingJoystick joystickInstance)
        {
            joystickInstance.transform.SetParent(joystickParent);
            joystickInstance.transform.localPosition = Vector3.zero;
            var rect = joystickInstance.GetComponent<RectTransform>();
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        private void BindWallet()
        {
            Container.Bind<Wallet.Wallet>().FromNew().AsSingle();
        }

        private void BindPlayer()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<Player.Player>(player);
            playerInstance.transform.position = spawnPoint.position;
            Container.Bind<Player.Player>().FromInstance(playerInstance).AsSingle();
            Container.QueueForInject(playerInstance);
        }

        private void BindEntryPoint()
        {
            Container.BindInterfacesTo<EntryPoint>().FromNew().AsSingle();
        }
    }
}
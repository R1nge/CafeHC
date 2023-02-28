using UnityEngine;
using Zenject;

namespace DI
{
    public class CoffeeFactoryInstaller : MonoInstaller
    {
        [SerializeField] private CoffeeFactory coffeeFactory;

        public override void InstallBindings()
        {
            var coffeeFactoryInstance = Container.InstantiatePrefabForComponent<CoffeeFactory>(coffeeFactory);
            Container.Bind<CoffeeFactory>().FromInstance(coffeeFactoryInstance).AsSingle();
            Container.QueueForInject(coffeeFactory);
        }
    }
}
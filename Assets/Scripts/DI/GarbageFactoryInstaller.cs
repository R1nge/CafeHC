using UnityEngine;
using Zenject;

namespace DI
{
    public class GarbageFactoryInstaller : MonoInstaller
    {
        [SerializeField] private GarbageFactory garbageFactory;

        public override void InstallBindings()
        {
            var garbageFactoryInstance = Container.InstantiatePrefabForComponent<GarbageFactory>(garbageFactory);
            Container.Bind<GarbageFactory>().FromInstance(garbageFactoryInstance).AsSingle();
            Container.QueueForInject(garbageFactory);
        }
    }
}
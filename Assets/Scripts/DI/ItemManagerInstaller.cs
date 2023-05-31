using Zenject;

namespace DI
{
    public class ItemManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //TODO: bind with interfaces
            Container.BindInterfacesAndSelfTo<ItemManager>().AsSingle().NonLazy();
        }
    }
}
using Zenject;

namespace DI
{
    public class InventoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle();
        }
    }
}
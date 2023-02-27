using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private Transform spawnPoint;

        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<Player.Player>(player);
            playerInstance.transform.SetParent(spawnPoint);
            playerInstance.transform.localPosition = Vector3.zero;
            Container.Bind<GameObject>().FromInstance(playerInstance.gameObject).AsSingle().NonLazy();
            Container.QueueForInject(player);
        }
    }
}
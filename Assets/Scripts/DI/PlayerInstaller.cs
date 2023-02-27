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
            playerInstance.transform.position = spawnPoint.position;
            Container.Bind<Player.Player>().FromInstance(playerInstance).AsSingle();
            Container.QueueForInject(player);
        }
    }
}
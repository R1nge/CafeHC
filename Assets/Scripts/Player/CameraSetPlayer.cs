using Cinemachine;
using UnityEngine;
using Zenject;

namespace Player
{
    public class CameraSetPlayer : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera vCamera;

        [Inject]
        public void Construct(Player player) => vCamera.Follow = player.transform;
    }
}
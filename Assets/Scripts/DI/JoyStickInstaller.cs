using UnityEngine;
using Zenject;

namespace DI
{
    public class JoyStickInstaller : MonoInstaller
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private Transform parent;

        public override void InstallBindings()
        {
            var joystickInstance = Container.InstantiatePrefabForComponent<FloatingJoystick>(floatingJoystick);
            joystickInstance.transform.SetParent(parent);
            joystickInstance.transform.localPosition = Vector3.zero;
            Container.Bind<FloatingJoystick>().FromInstance(joystickInstance).AsSingle();
            Container.QueueForInject(floatingJoystick);
        }
    }
}
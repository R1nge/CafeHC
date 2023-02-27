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
            Setup(joystickInstance);
            Container.Bind<FloatingJoystick>().FromInstance(joystickInstance).AsSingle();
            Container.QueueForInject(floatingJoystick);
        }

        private void Setup(FloatingJoystick joystickInstance)
        {
            joystickInstance.transform.SetParent(parent);
            joystickInstance.transform.localPosition = Vector3.zero;
            var rect = joystickInstance.GetComponent<RectTransform>();
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }
}
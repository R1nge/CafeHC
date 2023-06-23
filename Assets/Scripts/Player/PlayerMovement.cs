using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        private FloatingJoystick _floatingJoystick;
        private CharacterController _characterController;
        private Vector3 _movementDirection;

        [Inject]
        private void Construct(FloatingJoystick floatingJoystick) => _floatingJoystick = floatingJoystick;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void Update() => Move();

        private void Move()
        {
            _movementDirection = new(
                _floatingJoystick.Horizontal * movementSpeed,
                0,
                _floatingJoystick.Vertical * movementSpeed
            );

            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                Rotate();
                //TODO: Play anim
            }

            _characterController.Move(_movementDirection * Time.deltaTime);
        }

        private void Rotate()
        {
            var direction =
                Vector3.RotateTowards(
                    transform.forward,
                    _movementDirection,
                    rotationSpeed * Time.deltaTime,
                    0
                );

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
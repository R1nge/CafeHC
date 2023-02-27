using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        private FloatingJoystick _floatingJoystick;
        private Rigidbody _rigidbody;
        private Vector3 _movementDirection;

        [Inject]
        public void Constructor(FloatingJoystick floatingJoystick) => _floatingJoystick = floatingJoystick;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate() => Move();

        private void Move()
        {
            _movementDirection = new Vector3(
                _floatingJoystick.Horizontal * movementSpeed,
                0,
                _floatingJoystick.Vertical * movementSpeed
            ) * Time.deltaTime;

            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                Rotate();
                //Play anim
            }

            _rigidbody.MovePosition(_rigidbody.position + _movementDirection);
        }

        private void Rotate()
        {
            var direction =
                Vector3.RotateTowards(
                    transform.forward, _movementDirection,
                    rotationSpeed * Time.deltaTime, 0
                );

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
using UnityEngine;

namespace AI
{
    public class CustomerGoHome : State
    {
        private readonly CustomerMovement _movement;
        private readonly Vector3 _position;

        public CustomerGoHome(CustomerMovement movement, Vector3 position)
        {
            _movement = movement;
            _position = position;
        }

        public override void Enter() => _movement.MoveTo(_position);
    }
}
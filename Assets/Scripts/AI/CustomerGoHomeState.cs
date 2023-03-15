using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class CustomerGoHomeState : IState
    {
        private readonly CustomerMovement _movement;
        private readonly Vector3 _targetPosition;

        public CustomerGoHomeState(CustomerMovement movement, Vector3 targetPosition)
        {
            _movement = movement;
            _targetPosition = targetPosition;
        }

        public void Enter()
        {
            _movement.MoveTo(_targetPosition);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
        }
    }
}
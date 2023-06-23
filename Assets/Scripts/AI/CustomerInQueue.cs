using UnityEngine;

namespace AI
{
    public class CustomerInQueue : State
    {
        private readonly CustomerStateManager _stateManager;
        private readonly CustomerMovement _movement;
        private readonly Waypoints _waypoints;

        public CustomerInQueue(CustomerStateManager stateManager, CustomerMovement movement)
        {
            _stateManager = stateManager;
            _movement = movement;
        }

        public override void Enter() => _movement.MoveToNextWaypoint();

        public override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Counter _))
            {
                _stateManager.MakeOrder();
            }
        }
    }
}
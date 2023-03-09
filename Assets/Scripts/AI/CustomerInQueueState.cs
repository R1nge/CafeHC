using UnityEngine;

namespace AI
{
    public class CustomerInQueueState : IState
    {
        private readonly CustomerStateManager _stateManager;
        private readonly CustomerMovement _movement;
        private readonly Waypoints _waypoints;

        public CustomerInQueueState(CustomerStateManager stateManager, CustomerMovement movement, Waypoints waypoints)
        {
            _stateManager = stateManager;
            _movement = movement;
            _waypoints = waypoints;
        }

        public void Enter()
        {
            _waypoints.OnCustomerRemoved += _movement.MoveToNextWaypoint;
            _movement.MoveToNextWaypoint();
        }

        public void Exit()
        {
            _waypoints.OnCustomerRemoved -= _movement.MoveToNextWaypoint;
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Counter counter))
            {
                _stateManager.SetCustomerAtCounter();
            }
        }
    }
}
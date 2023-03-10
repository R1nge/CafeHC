using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class CustomerMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private int _currentIndex;

        //TODO: inject
        private Waypoints _waypoints;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _waypoints = FindObjectOfType<Waypoints>();
            _currentIndex = _waypoints.GetLastIndex();
            _waypoints.AddCustomer(this);
        }

        public void MoveToNextWaypoint()
        {
            MoveTo(_waypoints.GetWaypoints()[_currentIndex--].position);
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }
    }
}
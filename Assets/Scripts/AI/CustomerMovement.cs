using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class CustomerMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }
    }
}
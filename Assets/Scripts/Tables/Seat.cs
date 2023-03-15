using AI;
using UnityEngine;

namespace Tables
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Transform position;
        private CustomerStateManager _customer;

        public void SetCustomer(CustomerStateManager customer) => _customer = customer;

        public CustomerStateManager GetCustomer() => _customer;

        public Vector3 GetPosition() => position.position;
    }
}
using UnityEngine;

namespace AI
{
    public class CustomerOrder
    {
        private readonly CustomerMovement _customerMovement;

        public CustomerOrder(CustomerMovement customerMovement)
        {
            _customerMovement = customerMovement;
        }

        public void MakeOrder()
        {
            //Pick random amount of 
            Debug.Log(Random.Range(5,25));
            //_customerMovement.MoveTo(_tableSeatsManager.GetPosition());
        }
    }
}
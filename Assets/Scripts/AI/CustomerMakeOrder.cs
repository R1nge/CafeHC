using UnityEngine;

namespace AI
{
    public class CustomerMakeOrder : State
    {
        private readonly Inventory _inventory;

        public CustomerMakeOrder(Inventory inventory)
        {
            _inventory = inventory;
        }

        public override void Enter()
        {
            _inventory.SetSize(Random.Range(1, 1));
        }
    }
}
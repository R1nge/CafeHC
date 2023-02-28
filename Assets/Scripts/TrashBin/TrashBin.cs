using UnityEngine;
using Zenject;

namespace TrashBin
{
    public class TrashBin : MonoBehaviour
    {
        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory) => _inventory = inventory;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player _))
            {
                _inventory.RemoveAllItems();
            }
        }
    }
}
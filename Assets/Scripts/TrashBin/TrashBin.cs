using UnityEngine;

namespace TrashBin
{
    public class TrashBin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Inventory inventory))
            {
                inventory.RemoveAllItems();
            }
        }
    }
}
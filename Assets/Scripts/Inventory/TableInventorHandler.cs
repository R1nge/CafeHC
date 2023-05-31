using UnityEngine;

namespace AI
{
    public class TableInventorHandler : InventoryHandler
    {
        private int _currentCount;

        protected override void OnItemAdded(InventoryItem item)
        {
            var pos = new Vector3(
                transform.position.x,
                transform.position.y + 1f + 0.5f * _currentCount,
                transform.position.z
            );

            item.GetFromPool(pos, Quaternion.identity, transform);
            _currentCount++;
        }

        protected override void OnItemRemoved(InventoryItem item)
        {
            item.ReturnToPool(transform.GetChild(transform.childCount - 1).gameObject);
            _currentCount--;
        }

        protected override void OnAllItemsRemoved(InventoryItem item)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                item.ReturnToPool(transform.GetChild(i).gameObject);
            }
        }
    }
}
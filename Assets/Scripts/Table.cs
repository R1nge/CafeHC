using UnityEngine;
using Zenject;

public class Table : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    private Inventory _inventory;
    private CoffeeFactory _coffeeFactory;
    private int _currentCount;

    [Inject]
    public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Awake() => _inventory = GetComponent<Inventory>();

    private void OnAllItemsRemoved()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _coffeeFactory.ReturnToPool(transform.GetChild(i).gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory otherInventory))
        {
            if (_currentCount >= 5)
            {
                _inventory.RemoveAllItems();
                OnAllItemsRemoved();
                _currentCount = 0;
                return;
            }

            if (otherInventory.TryTransferTo(otherInventory.TakeItem(item), _inventory))
            {
                var pos = new Vector3(
                    transform.position.x,
                    transform.position.y + 0.1f + 0.05f * _currentCount,
                    transform.position.z
                );

                _coffeeFactory.GetFromPool(pos, Quaternion.identity, transform);
                _currentCount++;
            }
        }
    }
}
using UnityEngine;
using Zenject;

public class Table : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private Inventory inventory;
    private Inventory _playerInventory;
    private CoffeeFactory _coffeeFactory;
    private int _currentCount;

    [Inject]
    public void Construct(Inventory _inventory, CoffeeFactory coffeeFactory)
    {
        _playerInventory = _inventory;
        _coffeeFactory = coffeeFactory;
    }

    private void Start() => inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;

    private void OnAllItemsRemoved()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _coffeeFactory.ReturnToPool(transform.GetChild(i).gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player.Player player))
        {
            if (_currentCount >= 5)
            {
                inventory.RemoveAllItems();
                _currentCount = 0;
                return;
            }

            if (_playerInventory.TryTransferTo(_playerInventory.TakeItem(item), inventory))
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

    private void OnDestroy() => inventory.OnAllItemsRemovedEvent -= OnAllItemsRemoved;
}
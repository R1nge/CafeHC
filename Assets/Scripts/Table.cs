using System.Collections;
using UnityEngine;
using Zenject;

public class Table : MonoBehaviour
{
    [SerializeField] private float eatInterval;
    [SerializeField] private GameObject garbage;
    [SerializeField] private MoneyArea moneyArea;
    [SerializeField] private InventoryItem garbageItem;
    private Inventory _inventory;
    private CoffeeFactory _coffeeFactory;
    private PlayerInventory _playerInventory;
    private int _currentCount;
    private int _eatenCount;
    private YieldInstruction _coroutine;
    private bool _isAvailable = true;

    [Inject]
    private void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        _inventory.OnItemAddedEvent += OnItemAdded;
        _inventory.OnItemRemovedEvent += OnItemRemoved;
    }

    private void OnItemAdded(InventoryItem item)
    {
        var pos = new Vector3(
            transform.position.x,
            transform.position.y + 0.1f + 0.05f * _currentCount,
            transform.position.z
        );

        _coffeeFactory.GetFromPool(pos, Quaternion.identity, transform);
        _currentCount = _inventory.GetCount();

        _coroutine ??= StartCoroutine(Eat_c());
    }

    private void OnAllItemsRemoved()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _coffeeFactory.ReturnToPool(transform.GetChild(i).gameObject);
        }

        _currentCount = 0;
    }

    private void OnItemRemoved(InventoryItem item)
    {
        _coffeeFactory.ReturnToPool(transform.GetChild(transform.childCount - 1).gameObject);
        _currentCount--;
        moneyArea.AddMoney(Random.Range(5, 10));
    }

    private IEnumerator Eat_c()
    {
        _isAvailable = false;
        while (_currentCount >= 0)
        {
            if (_currentCount == 0)
            {
                EnableTrash();
                _coroutine = null;
                yield break;
            }

            yield return new WaitForSeconds(eatInterval);

            _inventory.RemoveItem(_inventory.GetItem());
            _eatenCount++;
        }
    }

    private void EnableTrash()
    {
        garbage.SetActive(true);
    }

    private void Clean()
    {
        if (_playerInventory.GetCount() != 0)
        {
            if (_playerInventory.GetItem().GetItemType() != InventoryItem.ItemType.Garbage)
            {
                return;
            }
        }
       
        
        for (int i = 0; i < _eatenCount; i++)
        {
            _playerInventory.TryAddItem(garbageItem);
        }

        _eatenCount = 0;
        garbage.SetActive(false);
        _isAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CustomerInventory customerInventory))
        {
            if (!_isAvailable) return;
            var count = customerInventory.GetCount();
            for (int i = count - 1; i >= 0; i--)
            {
                customerInventory.TryTransferTo(_inventory);
            }
        }
        else if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            if (garbage.activeInHierarchy)
            {
                _playerInventory = playerInventory;
                Clean();
            }
        }
    }

    private void OnDestroy()
    {
        _inventory.OnAllItemsRemovedEvent -= OnAllItemsRemoved;
        _inventory.OnItemAddedEvent -= OnItemAdded;
        _inventory.OnItemRemovedEvent -= OnItemRemoved;
    }
}
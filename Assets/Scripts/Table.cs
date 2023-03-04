using System.Collections;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private float eatInterval;
    [SerializeField] private GameObject garbage;
    [SerializeField] private MoneyArea moneyArea;
    private GarbageItem _garbageItem = new();
    private Inventory _inventory;
    private PlayerInventory _playerInventory;
    private int _currentCount;
    private int _eatenCount;
    private YieldInstruction _coroutine;
    private bool _isAvailable = true;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        _inventory.OnItemAddedEvent += OnItemAdded;
        _inventory.OnItemRemovedEvent += OnItemRemoved;
    }

    private void OnItemAdded(InventoryItem item)
    {
        _inventory.GetFromPool(item, GetPosition(), Quaternion.identity, transform);
        _currentCount = _inventory.GetCount();

        _coroutine ??= StartCoroutine(Eat_c());
    }

    private Vector3 GetPosition()
    {
        return new Vector3(
            transform.position.x,
            transform.position.y + 0.1f + 0.05f * _currentCount,
            transform.position.z
        );
    }

    private void OnAllItemsRemoved(InventoryItem item)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _inventory.ReturnToPool(item, transform.GetChild(i).gameObject);
        }

        _currentCount = 0;
    }

    private void OnItemRemoved(InventoryItem item)
    {
        _inventory.ReturnToPool(item, transform.GetChild(transform.childCount - 1).gameObject);
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
                garbage.SetActive(true);
                _coroutine = null;
                yield break;
            }

            yield return new WaitForSeconds(eatInterval);

            _inventory.RemoveItem(_inventory.GetItem());
            _eatenCount++;
        }
    }

    private void Clean()
    {
        if (_playerInventory.GetCount() != 0)
        {
            if (!_playerInventory.GetItem().CompareType(InventoryItem.ItemType.Garbage))
            {
                return;
            }
        }

        for (int i = 0; i < _eatenCount; i++)
        {
            _playerInventory.TryAddItem(_garbageItem);
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
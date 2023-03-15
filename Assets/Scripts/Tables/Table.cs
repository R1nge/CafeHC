using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AI;
using UnityEngine;
using Zenject;

namespace Tables
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private float eatInterval;
        [SerializeField] private GameObject garbage;
        [SerializeField] private MoneyArea moneyArea;
        [SerializeField] private List<Seat> seats;
        private readonly GarbageItem _garbageItem = new();
        private Inventory _inventory;
        private CoffeeFactory _coffeeFactory;
        private int _currentCount;
        private int _eatenCount;
        private YieldInstruction _coroutine;

        [Conditional("UNITY_EDITOR")]
        public void FindSeats()
        {
            seats = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Seat seat))
                {
                    seats.Add(seat);
                }
            }
        }

        public bool HasFreeSeat()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                if (seats[i].GetCustomer() || garbage.activeInHierarchy)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public Seat GetFreeSeat()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                if (seats[i].GetCustomer() || garbage.activeInHierarchy)
                {
                    continue;
                }

                return seats[i];
            }

            return null;
        }

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
                transform.position.y + 1f + 0.5f * _currentCount,
                transform.position.z
            );

            _coffeeFactory.GetFromPool(pos, Quaternion.identity, transform);
            _currentCount = _inventory.GetCount();

            if (HasFreeSeat()) return;
            _coroutine ??= StartCoroutine(Eat_c());
        }

        private void OnAllItemsRemoved(InventoryItem item)
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
            _eatenCount++;
            //moneyArea.AddMoney(Random.Range(5, 10));
        }

        private IEnumerator Eat_c()
        {
            while (_currentCount >= 0)
            {
                if (_currentCount == 0)
                {
                    EnableTrash();
                    FreeUpTable();
                    StopAllCoroutines();
                    _coroutine = null;
                    yield break;
                }

                yield return new WaitForSeconds(eatInterval);

                _inventory.RemoveItem(_inventory.GetItem());
            }
        }

        private void EnableTrash()
        {
            garbage.SetActive(true);
        }

        private void FreeUp()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                seats[i].GetCustomer().SetCustomerGoHome();
                seats[i].SetCustomer(null);
            }
        }

        private void Clean(PlayerInventory inventory)
        {
            if(!inventory.TryAddItem(_garbageItem)) return;
            for (int i = 0; i < _eatenCount; i++)
            {
                inventory.TryAddItem(_garbageItem);
            }

            _eatenCount = 0;
            garbage.SetActive(false);
        }

        private void FreeUpTable()
        {
            FreeUp();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CustomerInventory customerInventory))
            {
                if (customerInventory.TryGetComponent(out CustomerStateManager customerStateManager))
                {
                    if (!HasFreeSeat()) return;
                    GetFreeSeat().SetCustomer(customerStateManager);
                    var count = customerInventory.GetCount();
                    for (int i = count - 1; i >= 0; i--)
                    {
                        customerInventory.TryTransferTo(_inventory);
                    }
                }
            }
            else if (other.TryGetComponent(out PlayerInventory playerInventory))
            {
                if (garbage.activeInHierarchy)
                {
                    Clean(playerInventory);
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
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AI;
using UnityEngine;
using Zenject;

namespace Tables
{
    [RequireComponent(typeof(TableInventory))]
    public class Table : MonoBehaviour
    {
        [SerializeField] private float eatInterval;
        [SerializeField] private GameObject garbage;
        [SerializeField] private MoneyArea moneyArea;
        [SerializeField] private List<Seat> seats;
        private Inventory _inventory;
        private int _currentCount;
        private int _eatenCount;
        private YieldInstruction _coroutine;
        private ItemManager _itemManager;

        [Inject]
        private void Construct(ItemManager itemManager)
        {
            _itemManager = itemManager;
        }

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

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemAddedEvent += OnItemAdded;
            _inventory.OnItemRemovedEvent += OnItemRemoved;
            _inventory.OnAllItemsRemovedEvent += OnAllItemsRemoved;
        }

        private void OnItemAdded(InventoryItem item)
        {
            _currentCount++;
            print(_currentCount);
            if (HasFreeSeat()) return;
            _coroutine ??= StartCoroutine(Eat_c());
        }

        private void OnAllItemsRemoved(InventoryItem item)
        {
            _currentCount = 0;
        }

        private void OnItemRemoved(InventoryItem item)
        {
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
                    garbage.SetActive(true);
                    FreeUpTable();
                    StopAllCoroutines();
                    _coroutine = null;
                    yield break;
                }

                yield return new WaitForSeconds(eatInterval);

                _inventory.RemoveItem(_inventory.GetItem());
            }
        }

        private void FreeUpTable()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                seats[i].GetCustomer().SetCustomerGoHome();
                //seats[i].GetCustomer().SetCustomerInQueue();
                seats[i].SetCustomer(null);
            }
        }

        private void Clean(PlayerInventory inventory)
        {
            for (int i = 0; i < _eatenCount; i++)
            {
                inventory.TryAddItem(_itemManager.GetItem(InventoryItemType.Garbage));
            }

            _eatenCount = 0;
            garbage.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory))
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
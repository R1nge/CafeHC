using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CoffeeMachineInventory))]
public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private float delay;
    private Inventory _inventory;
    private ItemManager _itemManager;

    [Inject]
    private void Construct(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }
    
    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void Start() => StartCoroutine(Spawn_c());

    private IEnumerator Spawn_c()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(delay);
            _inventory.TryAddItem(_itemManager.GetItem(InventoryItemType.Coffee));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Inventory inventory))
        {
            var count = _inventory.GetCount();
            for (int i = 0; i < count; i++)
            {
                _inventory.TryTransferTo(inventory);
            }
        }
    }
}
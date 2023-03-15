using System.Collections;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float offsetX, offsetY;
    private Inventory _inventory;
    private readonly CoffeeItem _coffeeItem = new();

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.OnItemAddedEvent += Spawn;
        _inventory.OnItemRemovedEvent += DeSpawn;
    }

    private void Spawn(InventoryItem item)
    {
        _inventory.GetFromPool(_coffeeItem, GetPosition(), Quaternion.identity, spawnPoint);
    }

    private void DeSpawn(InventoryItem item)
    {
        _inventory.ReturnToPool(item, spawnPoint.GetChild(spawnPoint.childCount - 1).gameObject);
    }

    private void Start() => StartCoroutine(Spawn_c());

    private IEnumerator Spawn_c()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(delay);
            _inventory.TryAddItem(_coffeeItem);
        }
    }

    private Vector3 GetPosition()
    {
        var position = spawnPoint.position - Vector3.right * offsetX / 2f;
        var count = _inventory.GetCount();

        if (count % 2 == 0)
        {
            position += Vector3.right * offsetX;
            position += new Vector3(0, offsetY * Mathf.Floor((count - 1) / 2f));
            return position;
        }

        if (count % 2 == 1)
        {
            position += new Vector3(0, offsetY * Mathf.Floor(count / 2f));
        }

        return position;
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

    private void OnDestroy()
    {
        _inventory.OnItemAddedEvent -= Spawn;
        _inventory.OnItemRemovedEvent -= DeSpawn;
    }
}
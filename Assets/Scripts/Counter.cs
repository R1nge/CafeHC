using UnityEngine;

[RequireComponent(typeof(CounterInventory))]
public class Counter : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            TransferFrom(playerInventory);
        }
        else if (other.TryGetComponent(out CustomerInventory customerInventory))
        {
            TransferTo(customerInventory);
        }
    }

    private void TransferFrom(PlayerInventory playerInventory)
    {
        if (playerInventory.GetCount() == 0) return;
        var count = playerInventory.GetCount();
        for (int i = 0; i < count; i++)
        {
            playerInventory.TryTransferTo(_inventory);
        }
    }

    private void TransferTo(CustomerInventory customerInventory)
    {
        var count = _inventory.GetCount();
        for (int i = 0; i < count; i++)
        {
            _inventory.TryTransferTo(customerInventory);
        }
    }
}
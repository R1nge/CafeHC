using UnityEngine;
using Zenject;

public class Money : MonoBehaviour
{
    private Wallet _wallet;

    [Inject]
    public void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player.Player player))
        {
            _wallet.Earn(100);
            Destroy(gameObject);
            //TODO: pool
        }
    }
}
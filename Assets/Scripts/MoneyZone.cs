using UnityEngine;
using Zenject;

public class MoneyZone : MonoBehaviour
{
    [SerializeField] private Money money;
    private DiContainer _diContainer;
    
    [Inject]
    public void Construct(DiContainer diContainer) => _diContainer = diContainer;

    private void Start()
    {
        _diContainer.InstantiatePrefab(money, transform.position, Quaternion.identity, null);
    }
}
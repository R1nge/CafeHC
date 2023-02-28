using System.Collections;
using UnityEngine;
using Zenject;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private GameObject coffee;
    [SerializeField] private float delay;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCount;
    private int _currentCount;
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer) => _diContainer = diContainer;

    private void Start() => StartCoroutine(Spawn_c());

    private IEnumerator Spawn_c()
    {
        while (enabled)
        {
            _currentCount = spawnPoint.childCount;
            yield return new WaitForSeconds(delay);
            if (_currentCount < maxCount)
            {
                _diContainer.InstantiatePrefab(coffee, spawnPoint.position, Quaternion.identity, null);
            }
        }
    }
}
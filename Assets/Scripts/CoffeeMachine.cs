using System.Collections;
using UnityEngine;
using Zenject;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCount;
    private int _currentCount;
    private CoffeeFactory _coffeeFactory;

    [Inject]
    private void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Start() => StartCoroutine(Spawn_c());

    private IEnumerator Spawn_c()
    {
        while (enabled)
        {
            _currentCount = spawnPoint.childCount;
            yield return new WaitForSeconds(delay);
            if (_currentCount < maxCount)
            {
                //TODO: spawn in different positions
                _coffeeFactory.GetFromPool(spawnPoint.position, Quaternion.identity, spawnPoint);
            }
        }
    }
}
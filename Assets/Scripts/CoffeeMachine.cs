using System.Collections;
using UnityEngine;
using Zenject;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private CoffeeCup coffee;
    [SerializeField] private float delay;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCount;
    private int _currentCount;
    private CoffeeFactory _coffeeFactory;

    [Inject]
    public void Construct(CoffeeFactory coffeeFactory) => _coffeeFactory = coffeeFactory;

    private void Start()
    {
        //TODO: Find optimal pool size
        _coffeeFactory.CreatePool(coffee, 30);
        StartCoroutine(Spawn_c());
    }

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
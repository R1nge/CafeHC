using UnityEngine;
using Zenject;

public class PoolInitializer : MonoBehaviour
{
    [SerializeField] private CoffeeCup coffee;
    [SerializeField] private Garbage garbage;
    private CoffeeFactory _coffeeFactory;
    private GarbageFactory _garbageFactory;

    [Inject]
    private void Construct(CoffeeFactory coffeeFactory, GarbageFactory garbageFactory)
    {
        _coffeeFactory = coffeeFactory;
        _garbageFactory = garbageFactory;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _coffeeFactory.CreatePool(coffee, 30);
        _garbageFactory.CreatePool(garbage, 30);
    }
}
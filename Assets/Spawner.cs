using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject model1;
    private Dictionary<string, GameObject> _models = new();

    private void Start()
    {
        var instance = Instantiate(model1);
        _models.Add("Coffee" , instance);
    }

    public void Spawn(Transform parent)
    {
        Instantiate(_models["Coffee"], parent);
    }
}
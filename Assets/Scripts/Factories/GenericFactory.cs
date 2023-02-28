using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenericFactory : MonoBehaviour
{
    private List<GameObject> _pool;
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer) => _diContainer = diContainer;

    public GameObject GetNewInstance(GameObject go, Vector3 pos, Quaternion rot, Transform parent)
    {
        return _diContainer.InstantiatePrefab(go, pos, rot, parent);
    }

    public void CreatePool(GameObject go, int amount)
    {
        if (_pool != null)
        {
            Debug.LogWarning("Trying to create pool, when one already exists");
            return;
        }

        _pool = new List<GameObject>(amount);

        for (int i = 0; i < amount; i++)
        {
            var instance = _diContainer.InstantiatePrefab(go, Vector3.zero, Quaternion.identity, null);
            instance.SetActive(false);
            _pool.Add(instance);
        }
    }

    public void ChangePoolSize(int amount)
    {
        var previousSize = _pool.Capacity;
        _pool.Capacity += amount;

        if (previousSize > previousSize + amount)
        {
            for (int i = previousSize - amount - 1; i >= 0; i--)
            {
                //Destroy(_pool[i].gameObject);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                //GetNewInstance(_pool[0].gameObject, Vector3.zero, Quaternion.identity, null);
            }
        }
    }

    public GameObject GetFromPool(Vector3 pos, Quaternion rot, Transform parent)
    {
        if (_pool == null)
        {
            Debug.LogError("Trying to get object from uninitialized pool");
            return null;
        }

        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].activeInHierarchy)
            {
                continue;
            }

            var _transform = _pool[i].transform;
            _transform.SetPositionAndRotation(pos, rot);
            _transform.SetParent(parent);
            _pool[i].SetActive(true);
            return _pool[i];
        }

        return null;
    }

    public void ReturnToPool(GameObject go) => go.SetActive(false);
}
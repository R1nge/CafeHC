using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<T> _pool;
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer) => _diContainer = diContainer;

    public T GetNewInstance(T go, Vector3 pos, Quaternion rot, Transform parent)
    {
        var instance = _diContainer.InstantiatePrefab(go, pos, rot, parent);
        instance.SetActive(true);
        return instance.GetComponent<T>();
    }

    public void CreatePool(T go, int amount)
    {
        if (_pool != null)
        {
            Debug.LogWarning("Trying to create pool, when one already exists", this);
            return;
        }

        _pool = new List<T>(amount);

        for (int i = 0; i < amount; i++)
        {
            var instance = _diContainer.InstantiatePrefab(go.gameObject, Vector3.zero, Quaternion.identity, null);
            instance.SetActive(false);
            _pool.Add(instance.GetComponent<T>());
        }
    }

    public void SetPoolSize(int value)
    {
        if (value < 0)
        {
            Debug.LogError("Trying to set negative size", this);
            return;
        }

        var previousSize = _pool.Capacity;

        if (previousSize > value)
        {
            for (int i = previousSize - value - 1; i >= 0; i--)
            {
                Destroy(_pool[i].gameObject);
                _pool.RemoveAt(i);
            }

            _pool.Capacity = value;
        }
        else if (previousSize < previousSize + value)
        {
            for (int i = 0; i < value; i++)
            {
                AddToPool();
            }
        }
    }

    private void AddToPool()
    {
        var instance = GetNewInstance(_pool[0], Vector3.zero, Quaternion.identity, null);
        instance.gameObject.SetActive(false);
        _pool.Add(instance);
    }

    public T GetFromPool(Vector3 pos, Quaternion rot, Transform parent)
    {
        if (_pool == null)
        {
            Debug.LogError("Trying to get object from uninitialized pool", this);
            return null;
        }

        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].gameObject.activeInHierarchy)
            {
                if (i == _pool.Capacity - 1)
                {
                    Debug.LogError("Every pooled object is active, can't get one", this);
                    break;
                }

                continue;
            }

            var _transform = _pool[i].transform;
            _transform.SetPositionAndRotation(pos, rot);
            _transform.SetParent(parent);
            _pool[i].gameObject.SetActive(true);
            return _pool[i];
        }

        return null;
    }

    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(null);
    }
}
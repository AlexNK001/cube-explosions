using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner<T> where T : Item
{
    private readonly T _prefab;
    private readonly ObjectPool<T> _objectPool;
    private readonly IPositionGenerator _positionGenerator;
    private readonly List<Item> _items;

    private int _numberObjectAppearances;

    public Action<ViewInfo> InformationUpdated;

    public ItemSpawner(IPositionGenerator positionGenerator, T prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
    {
        _positionGenerator = positionGenerator;
        _positionGenerator.GetPosition += Get;

        _items = new List<Item>();

        _prefab = prefab;

        _objectPool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (item) => ActionOnGet(item),
            actionOnRelease: (item) => ActionOnRelease(item),
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize);
    }

    public void Unsubscribe()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].TimeLifeIsOver -= Release;
        }
    }

    private void Get(Vector3 position)
    {
        T item = _objectPool.Get();
        item.transform.position = position;
    }

    protected virtual void Release(Item item)
    {
        _objectPool.Release(item as T);
    }

    private T CreateFunc()
    {
        T poolObject = MonoBehaviour.Instantiate(_prefab);
        poolObject.gameObject.SetActive(false);
        poolObject.TimeLifeIsOver += Release;
        _items.Add(poolObject);
        return poolObject;
    }

    private void ActionOnGet(T item)
    {
        _numberObjectAppearances++;
        InformationUpdated?.Invoke(GetViewInfo());
        item.gameObject.SetActive(true);
    }

    private void ActionOnRelease(T item)
    {
        InformationUpdated?.Invoke(GetViewInfo());
        item.gameObject.SetActive(false);
    }

    private ViewInfo GetViewInfo()
    {
        return new ViewInfo(_numberObjectAppearances, _objectPool.CountAll, _objectPool.CountActive);
    }
}

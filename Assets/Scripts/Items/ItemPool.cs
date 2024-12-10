using System;
using UnityEngine;
using UnityEngine.Pool;

public class ItemPool<T> where T : Item
{
    private readonly T _prefab;
    private readonly ObjectPool<T> _objectPool;
    private int _numberObjectAppearances;

    public Action<ViewInfo> InformationUpdated;

    public ItemPool(T prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
    {
        _prefab = prefab;

        _objectPool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (item) => ActionOnGet(item),
            actionOnRelease: (item) => ActionOnRelease(item),
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize);
    }

    public T Get(Vector3 position)
    {
        T item = _objectPool.Get();
        item.transform.position = position;
        return item;
    }

    public void Release(T item)
    {
        _objectPool.Release(item);
    }

    private T CreateFunc()
    {
        T poolObject = MonoBehaviour.Instantiate(_prefab);
        poolObject.gameObject.SetActive(false);
        poolObject.Init<T>(this);
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

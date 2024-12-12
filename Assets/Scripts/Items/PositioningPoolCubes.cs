using System;
using UnityEngine;

public class PositioningPoolCubes : ItemSpawner<Cube>, IPositionGenerator
{
    public PositioningPoolCubes(
        IPositionGenerator positionGenerator,
        Cube prefab,
        bool collectionCheck = true,
        int defaultCapacity = 10,
        int maxSize = 10000) : base(
            positionGenerator,
            prefab,
            collectionCheck,
            defaultCapacity,
            maxSize) { }

    public event Action<Vector3> GetPosition;

    protected override void Release(Item item)
    {
        base.Release(item);
        GetPosition.Invoke(item.transform.position);
    }
}

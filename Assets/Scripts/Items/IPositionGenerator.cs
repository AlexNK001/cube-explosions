using System;
using UnityEngine;

public interface IPositionGenerator
{
    public event Action<Vector3> GetPosition;
}

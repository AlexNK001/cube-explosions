//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PositionGenerator : MonoBehaviour
{
    //[Header("PositionGenerator")]
    [SerializeField] private float _hight = 17f;
    private int _xLength;
    private int _zLength;
    private Vector3 _surfaceAnglePosition;

    private void Start()
    {
        _xLength = Mathf.RoundToInt(transform.localScale.x);
        _zLength = Mathf.RoundToInt(transform.localScale.z);
        _surfaceAnglePosition = new(_xLength / -2, _hight, _zLength / -2);
    }

    public Vector3 GetRandomPosition()
    {
        float x = _surfaceAnglePosition.x + Random.Range(0f, _xLength);
        float y = _hight;
        float z = _surfaceAnglePosition.z + Random.Range(0f, _zLength);

        return new(x, y, z);
    }
}

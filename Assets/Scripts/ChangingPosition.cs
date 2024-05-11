using UnityEngine;

public class ChangingPosition : MonoBehaviour
{
    private const float Disection = -2f;

    [SerializeField] private float _hight = 17f;

    private int _xLength;
    private int _zLength;
    private Vector3 _surfaceAnglePosition;

    private void Awake()
    {
        _xLength = Mathf.RoundToInt(transform.localScale.x);
        _zLength = Mathf.RoundToInt(transform.localScale.z);
        _surfaceAnglePosition = new(_xLength / Disection, _hight, _zLength / Disection);
    }

    public Vector3 ChangeCubePosition()
    {
        float x = _surfaceAnglePosition.x + Random.Range(0, _xLength);
        float y = _hight;
        float z = _surfaceAnglePosition.z + Random.Range(0, _zLength);

        return new(x, y, z);
    }

    //protected override void Subscribe()
    //{
    //    _cubePool.CubeIssued += ChangeCubePosition;
    //}

    //protected override void Unsubscribe()
    //{
    //    _cubePool.CubeIssued -= ChangeCubePosition;
    //}
}

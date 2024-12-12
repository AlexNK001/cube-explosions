using UnityEngine;

public class PositionGenerator : IPositionGenerator
{
    private const float Disection = -2f;

    private readonly Timer _timer;
    private readonly float _yOffSet = 17f;
    private readonly int _xLength;
    private readonly int _zLength;
    private Vector3 _surfaceAnglePosition;

    public event System.Action<Vector3> GetPosition;

    public PositionGenerator(Timer timer, float yOffset, Transform transform)
    {
        _timer = timer;
        _timer.TimeHasCome += GetSpawnPosition;

        _yOffSet = yOffset;
        _xLength = Mathf.RoundToInt(transform.localScale.x);
        _zLength = Mathf.RoundToInt(transform.localScale.z);
        _surfaceAnglePosition = new(_xLength / Disection, _yOffSet, _zLength / Disection);
    }

    public void Unsubscribe()
    {
        _timer.TimeHasCome -= GetSpawnPosition;
    }

    private void GetSpawnPosition()
    {
        float x = _surfaceAnglePosition.x + Random.Range(0, _xLength);
        float y = _yOffSet;
        float z = _surfaceAnglePosition.z + Random.Range(0, _zLength);

        GetPosition.Invoke(new(x, y, z));
    }
}

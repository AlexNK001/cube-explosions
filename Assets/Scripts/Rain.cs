using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangingPosition), typeof(CubePool))]//, typeof(ColorChanging)
public class Rain : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAppearances = 0.2f;

    private WaitForSeconds _delayBetweenAppearances;
    private Coroutine _spawnCubes;

    private ChangingPosition _changingPosition;
    //private ColorChanging _colorChanging;
    private CubePool _cubePool;

    //private Dictionary<Cube, Coroutine> _waitingDisappear = new();
    private float _minDisappearanceTime = 2f;
    private float _maxDisappearanceTime = 5f;

    private void Awake()
    {
        _changingPosition = GetComponent<ChangingPosition>();
        //_colorChanging = GetComponent<ColorChanging>();
        _cubePool = GetComponent<CubePool>();
        _delayBetweenAppearances = new(_timeBetweenAppearances);
    }

    private void Start()
    {
        _spawnCubes = StartCoroutine(nameof(SpawnCubes));
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCubes);
    }

    internal void SetCube(Cube cube)
    {
        StartCoroutine(DelayDestroy(cube));
        //_colorChanging.SetCube(cube);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.TryGetComponent(out Cube cube) && TryContacted(cube) == false)
    //    {
    //        Coroutine coroutine = StartCoroutine(DelayDestroy(cube));
    //        _waitingDisappear.Add(cube, coroutine);
    //    }
    //}

    //public bool TryContacted(Cube cube)
    //{
    //    return _waitingDisappear.ContainsKey(cube);
    //}

    private IEnumerator DelayDestroy(Cube cube)
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(_minDisappearanceTime, _maxDisappearanceTime));
            _cubePool.Accept(cube);
            //StopCoroutine(_waitingDisappear[cube]);
            //_waitingDisappear.Remove(cube);
        }
    }

    private IEnumerator SpawnCubes()
    {
        while (gameObject.activeSelf)
        {
            yield return _delayBetweenAppearances;
            _cubePool.Issue();
        }
    }
}
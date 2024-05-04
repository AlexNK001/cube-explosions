using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Rain : MonoBehaviour
{
    [SerializeField] private float _hight = 17f;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10000;

    [SerializeField] private Color _startingColor = Color.green;
    [SerializeField] private Color _contactColor = Color.blue;

    private ObjectPool<Cube> _pool;
    private int _xLength;
    private int _zLength;
    private Vector3 _surfaceAnglePosition;

    [SerializeField] private float _delay = 0.2f;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _coroutine;
    private Dictionary<Cube, Coroutine> _listCoroutines = new();

    private void Awake()
    {
        _waitForSeconds = new(_delay);

        _pool = new ObjectPool<Cube>
            (
             () => CreatedCube(),
             (cube) => ActionOnGet(cube),
             (cube) => ActionOnRelease(cube),
             (cube) => ActionOnDestroy(cube),
             true,
             _poolCapacity,
             _poolMaxSize
            );
    }

    private Cube CreatedCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.Contact += CheakContact;
        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.SetColor(_startingColor);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Cube cube)
    {
        cube.Contact -= CheakContact;
    }

    private void CheakContact(Cube cube)
    {
        cube.SetColor(_contactColor);
        Coroutine coroutine = StartCoroutine(DelayDestroy(cube));
        _listCoroutines.Add(cube, coroutine);
    }

    private IEnumerator DelayDestroy(Cube cube)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            _pool.Release(cube);
            StopCoroutine(_listCoroutines[cube]);
            _listCoroutines.Remove(cube);
        }
    }

    private void Start()
    {
        _xLength = Mathf.RoundToInt(transform.localScale.x);
        _zLength = Mathf.RoundToInt(transform.localScale.z);
        _surfaceAnglePosition = new(_xLength / -2, _hight, _zLength / -2);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(nameof(CreatingCubes));
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private Vector3 GetRandomPosition()
    {
        float x = _surfaceAnglePosition.x + Random.Range(0f, _xLength);
        float y = _hight;
        float z = _surfaceAnglePosition.z + Random.Range(0f, _zLength);

        return new(x, y, z);
    }

    private IEnumerator CreatingCubes()
    {
        while (gameObject.activeSelf)
        {
            Cube cube = _pool.Get();
            cube.transform.position = GetRandomPosition();

            yield return _waitForSeconds;
        }
    }
}
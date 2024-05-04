using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionGenerator))]
[RequireComponent(typeof(ColorHandler))]
[RequireComponent(typeof(CubePool))]
public class Rain : MonoBehaviour
{
    private PositionGenerator _positionGenerator;
    private CubePool _cubePool;
    private ColorHandler _colorHandler;

    private Dictionary<Cube, Coroutine> _listCoroutines = new();
    private float _minDestroyTime = 2f;
    private float _maxDestroyTime = 5f;

    [SerializeField] private float _delay = 0.2f;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _coroutine;

    private void Awake()
    {
        _positionGenerator = GetComponent<PositionGenerator>();
        _colorHandler = GetComponent<ColorHandler>();
        _cubePool = GetComponent<CubePool>();

        _waitForSeconds = new(_delay);
    }

    private void CheakContact(Cube cube)
    {
        Coroutine coroutine = StartCoroutine(DelayDestroy(cube));
        _listCoroutines.Add(cube, coroutine);
    }

    private IEnumerator DelayDestroy(Cube cube)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minDestroyTime, _maxDestroyTime));
            _cubePool.Accept(cube);
            StopCoroutine(_listCoroutines[cube]);
            _listCoroutines.Remove(cube);
            cube.Contact -= CheakContact;
        }
    }

    private void Start()
    {
        _coroutine = StartCoroutine(nameof(CreatingCubes));
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator CreatingCubes()
    {
        while (gameObject.activeSelf)
        {
            Cube cube = _cubePool.Issue();
            cube.Contact += CheakContact;
            cube.transform.position = _positionGenerator.GetRandomPosition();

            yield return _waitForSeconds;
        }
    }
}
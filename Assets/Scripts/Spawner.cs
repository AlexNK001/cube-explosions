using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(CubeFuse))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _maxNumberCubes = 6;
    private int _minNumberCubes = 2;
    private CubeFuse _cubeFuse;

    private ObjectPool<Cube> _pool;
    private int _poolCapacity = 5;
    private int _poolMaxSize = 50;

    private Cube _targetCube;

    private void Start()
    {
        _cubeFuse = GetComponent<CubeFuse>();

        _pool = new ObjectPool<Cube>
           (
            () => CreateCube(),
            (cube) => ActionOnGet(cube),
            (cube) => cube.gameObject.SetActive(false),
            (cube) => cube.Divided -= SelectActionOnCube,
            true,
            _poolCapacity,
            _poolMaxSize
           );

        Cube[] cubes = GetComponentsInChildren<Cube>();

        foreach (Cube cube in cubes)
        {
            _pool.Release(cube);
            cube.Divided += SelectActionOnCube;
            _targetCube = cube;
            _pool.Get();
        }
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.Divided += SelectActionOnCube;
        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.CatchUp(_targetCube);
        cube.transform.SetPositionAndRotation(_targetCube.transform.position, Random.rotation);
    }

    private void SelectActionOnCube(Cube mainCube, bool isAlive)
    {
        _targetCube = mainCube;
        _pool.Release(mainCube);

        if (isAlive)
        {
            List<Cube> createdCubes = new();
            int number = Random.Range(_minNumberCubes, _maxNumberCubes);

            for (int i = 0; i < number; i++)
            {
                createdCubes.Add(_pool.Get());
                _cubeFuse.Scatter(createdCubes);
            }
        }
        else
        {
            _cubeFuse.BlowUp(mainCube);
        }
    }
}
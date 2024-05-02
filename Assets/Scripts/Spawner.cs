using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;

    private int _maxNumberCubes = 6;
    private int _minNumberCubes = 2;
    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;
    private int _numberNewCubes;

    private void Start()
    {
        foreach (Cube cube in _cubes)
        {
            cube.Divided += CheckClick;
        }
    }

    private void OnDestroy()
    {
        foreach (Cube cube in _cubes)
        {
            cube.Divided -= CheckClick;
        }
    }

    private void CheckClick(Cube mainCube)
    {
        List<Cube> newCubes = new();
        _numberNewCubes = Random.Range(_minNumberCubes, _maxNumberCubes);

        newCubes.AddRange(ReuseInactiveCubes(mainCube));

        if (_numberNewCubes > 0)
            newCubes.AddRange(CreateMissingCubes(mainCube));

        Scatter(newCubes);

        _cubes.AddRange(newCubes);
    }

    private List<Cube> ReuseInactiveCubes(Cube mainCube)
    {
        List<Cube> newCubes = new();

        foreach (Cube oldCube in _cubes)
        {
            if (oldCube.isActiveAndEnabled == false && _numberNewCubes > 0)
            {
                oldCube.gameObject.SetActive(true);
                oldCube.transform.position = mainCube.transform.position;

                Refresh(mainCube, newCubes, oldCube);

                _numberNewCubes--;
            }
        }

        return newCubes;
    }

    private List<Cube> CreateMissingCubes(Cube mainCube)
    {
        List<Cube> newCubes = new();

        for (int i = 0; i < _numberNewCubes; i++)
        {
            Cube newCube = Instantiate(mainCube, mainCube.transform.position, Random.rotation);
            Refresh(mainCube, newCubes, newCube);
        }

        return newCubes;
    }

    private void Refresh(Cube mainCube, List<Cube> newCubes, Cube item)
    {
        item.CatchUp(mainCube);
        item.Divided += CheckClick;
        newCubes.Add(item);
    }

    private void Scatter(List<Cube> newCubes)
    {
        foreach (Cube newCube in newCubes)
        {
            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}
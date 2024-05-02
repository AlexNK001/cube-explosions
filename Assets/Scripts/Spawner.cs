using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;

    private int _maxNumberCubes = 6;
    private int _minNumberCubes = 2;
    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;

    private void Start()
    {
        foreach (var item in _cubes)
        {
            item.Divided += CheckClick;
        }
    }

    private void OnDestroy()
    {
        foreach (Cube cube in _cubes)
        {
            cube.Divided -= CheckClick;
        }
    }

    private void CheckClick(Cube cube)
    {
        List<Cube> newCubes = new();

        for (int i = 0; i < Random.Range(_minNumberCubes, _maxNumberCubes); i++)
        {
            Cube newCube = Instantiate(cube, cube.transform.position, Random.rotation);
            newCube.CatchUp(cube);
            newCube.Divided += CheckClick;
            newCubes.Add(newCube);
        }

        foreach (Cube newCube in newCubes)
        {
            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        _cubes.AddRange(newCubes);
    }
}

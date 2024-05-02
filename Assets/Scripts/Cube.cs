using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float MaxPercentage = 100;
    private const float MinPercentage = 0;

    private int _maxNumberCubes = 6;
    private int _minNumberCubes = 1;
    private float _divisor = 2f;
    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;

    private float _divisionChance = 100;
    private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        if(_divisionChance >= Random.Range(MinPercentage, MaxPercentage))
        {
            _divisionChance /= _divisor;

            List<Rigidbody> newCubes = new();

            for (int i = 0; i < Random.Range(_minNumberCubes, _maxNumberCubes); i++)
            {
                Cube cube = Instantiate(this, transform.position, Random.rotation);
                cube.transform.localScale /= _divisor;
                newCubes.Add(cube.GetComponent<Rigidbody>());
            }

            transform.localScale /= _divisor;
            _meshRenderer.material.color = Random.ColorHSV();
            newCubes.Add(GetComponent<Rigidbody>());

            foreach (Rigidbody item in newCubes)
            {
                item.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
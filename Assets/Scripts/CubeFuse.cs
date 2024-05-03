using System.Collections.Generic;
using UnityEngine;

public class CubeFuse : MonoBehaviour
{
    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;

    public void Scatter(List<Cube> newCubes)
    {
        foreach (Cube newCube in newCubes)
        {
            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    public void BlowUp(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _explosionRadius / cube.Size.x);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(
                    _explosionForce / cube.Size.x,
                    cube.transform.position,
                    _explosionRadius / cube.Size.x);
            }
        }
    }
}
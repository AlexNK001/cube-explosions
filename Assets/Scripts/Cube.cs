using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private const float MaxPercentage = 1;
    private const float MinPercentage = 0;
    private const float _divisor = 2f;

    private MeshRenderer _meshRenderer;

    public event UnityAction<Cube, bool> Divided;
    public Vector3 Size => transform.localScale;

    private void OnEnable()
    {
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.material.color = Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        bool isAlive = Size.x >= Random.Range(MinPercentage, MaxPercentage);

        transform.localScale /= _divisor;
        Divided.Invoke(this, isAlive);
    }

    public void CatchUp(Cube cube)
    {
        transform.localScale = cube.Size;
    }
}
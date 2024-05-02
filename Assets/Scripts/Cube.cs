using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float MaxPercentage = 1;
    private const float MinPercentage = 0;

    private float _divisor = 2f;
    private MeshRenderer _meshRenderer;

    public Vector3 Size => transform.localScale;
    public event UnityAction<Cube> Divided;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        if (Size.x >= Random.Range(MinPercentage, MaxPercentage))
        {
            transform.localScale /= _divisor;
            Divided.Invoke(this);
        }

        gameObject.SetActive(false);
    }

    public void CatchUp(Cube cube)
    {
        transform.localScale = cube.Size;
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
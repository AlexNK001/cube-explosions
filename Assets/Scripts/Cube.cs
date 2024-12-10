using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _startingColor = Color.green;
    [SerializeField] private Color _contactColor = Color.blue;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private bool _isContact = false;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _isContact = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _meshRenderer.material.color = _startingColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Rain rain) && _isContact == false)
        {
            _isContact = true;
            rain.StartDisappearing(this);
            _meshRenderer.material.color = _contactColor;
        }
    }
}
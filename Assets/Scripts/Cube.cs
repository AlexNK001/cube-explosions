using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;

    private bool _isContact = false;

    public event UnityAction<Cube> Contact;

    private void OnEnable()
    {
        if(_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        if(_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        if(_boxCollider == null)
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        _isContact = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Rain>(out _) && _isContact == false)
        {
            _isContact = true;
            Contact.Invoke(this);
        }
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
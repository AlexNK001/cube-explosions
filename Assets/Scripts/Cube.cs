using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private bool _isContact = false;

    public event UnityAction<Cube> Contact;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
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
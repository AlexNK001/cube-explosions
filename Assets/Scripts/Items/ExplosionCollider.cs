using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ExplosionCollider : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private float _upwardsModofier;
    [SerializeField] private SphereCollider _sphereCollider;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _sphereCollider.radius, _upwardsModofier, _forceMode);
        }
    }
}

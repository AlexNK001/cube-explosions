using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Rigidbody Rigidbody;
    [SerializeField] protected MeshRenderer MeshRenderer;
    [SerializeField] protected Color StartingColor;
    [SerializeField] protected Color EndingColor;
    [SerializeField] protected float MinDuration;
    [SerializeField] protected float MaxDuration;

    public abstract void Init<T>(ItemPool<T> itemPool) where T : Item;

    protected void ResetMovements()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}

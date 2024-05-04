using UnityEngine;

public abstract class PullUser : MonoBehaviour
{
    private protected CubePool _cubePool;

    private void Start()
    {
        _cubePool = GetComponent<CubePool>();
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}
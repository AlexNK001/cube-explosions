using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10000;

    private ObjectPool<Cube> _pool;

    public event UnityAction<Cube> CubeIssued;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
             () => Instantiate(_cubePrefab),
             (cube) => ActionOnGet(cube),
             (cube) => cube.gameObject.SetActive(false),
             (cube) => Destroy(cube),
             true,
             _poolCapacity,
             _poolMaxSize
            );
    }

    public void Issue()
    {
        _pool.Get();
    }

    public void Accept(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        CubeIssued?.Invoke(cube);
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10000;

    private ObjectPool<Cube> _pool;

    public event UnityAction<Cube> CubeCreated;
    public event UnityAction<Cube> CubeIssued;
    public event UnityAction<Cube> CubeDestroyed;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
             () => CreateCube(),
             (cube) => ActionOnGet(cube),
             (cube) => cube.gameObject.SetActive(false),
             (cube) => CubeDestroyed.Invoke(cube),
             true,
             _poolCapacity,
             _poolMaxSize
            );
    }

    public Cube Issue()
    {
        return _pool.Get();
    }

    public void Accept(Cube cube)
    {
        _pool.Release(cube);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        CubeCreated.Invoke(cube);
        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        CubeIssued.Invoke(cube);
    }
}
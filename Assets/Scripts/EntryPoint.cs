using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _spawnYOffset = 19f;
    [SerializeField] private ShowingPool _showingCubePool;
    [SerializeField] private ShowingPool _showingBombPool;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Timer _timer;

    private PositionGenerator _positionGenerator;
    private PositioningPoolCubes _cubePool;
    private ItemSpawner<Bomb> _bombPool;

    private void Awake()
    {
        _positionGenerator = new PositionGenerator(_timer, _spawnYOffset, _mainPlatform.transform);
        _cubePool = new PositioningPoolCubes(_positionGenerator, _cubePrefab);
        _bombPool = new ItemSpawner<Bomb>(_cubePool, _bombPrefab);
    }

    private void Start()
    {
        _cubePool.InformationUpdated += _showingCubePool.Show;
        _bombPool.InformationUpdated += _showingBombPool.Show;
    }

    private void OnDestroy()
    {
        _positionGenerator.Unsubscribe();
        _cubePool.Unsubscribe();
        _bombPool.Unsubscribe();
    }
}

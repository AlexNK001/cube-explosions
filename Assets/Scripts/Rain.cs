using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private ShowingPool _showingCubePool;
    [SerializeField] private ShowingPool _showingBombPool;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private PositionGenerator _positionGenerator;

    [SerializeField] private float _timeBetweenAppearances = 0.2f;

    private WaitForSeconds _delayBetweenAppearances;
    private ItemPool<Cube> _cubePool;
    private ItemPool<Bomb> _bombPool;
    private List<Cube> _cubes;

    private void Awake()
    {
        _cubePool = new ItemPool<Cube>(_cubePrefab);
        _bombPool = new ItemPool<Bomb>(_bombPrefab);
        _cubes = new List<Cube>();
        _delayBetweenAppearances = new(_timeBetweenAppearances);
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
        _cubePool.InformationUpdated += _showingCubePool.Show;
        _bombPool.InformationUpdated += _showingBombPool.Show;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _cubes.Count; i++)
        {
            _cubes[i].TimeLifeIsOver -= ReplaceWithBomb;
        }
    }

    private IEnumerator SpawnCubes()
    {
        while (gameObject.activeSelf)
        {
            yield return _delayBetweenAppearances;
            Cube cube = _cubePool.Get(_positionGenerator.GetSpawnPosition());
            cube.TimeLifeIsOver += ReplaceWithBomb;
            _cubes.Add(cube);
        }
    }

    private void ReplaceWithBomb(Cube cube)
    {
        _bombPool.Get(cube.transform.position);
        cube.TimeLifeIsOver -= ReplaceWithBomb;
        _cubes.Remove(cube);
    }
}

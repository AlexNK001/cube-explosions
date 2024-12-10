using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubePool))]
public class Rain : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAppearances = 0.2f;
    [SerializeField] private float _minDisappearanceTime = 2f;
    [SerializeField] private float _maxDisappearanceTime = 5f;

    private WaitForSeconds _delayBetweenAppearances;
    private CubePool _cubePool;

    private void Awake()
    {
        _cubePool = GetComponent<CubePool>();
        _delayBetweenAppearances = new(_timeBetweenAppearances);
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    public void StartDisappearing(Cube cube)
    {
        StartCoroutine(DelayDisappearance(cube));
    }

    private IEnumerator DelayDisappearance(Cube cube)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minDisappearanceTime, _maxDisappearanceTime));
        _cubePool.Accept(cube);
    }

    private IEnumerator SpawnCubes()
    {
        while (gameObject.activeSelf)
        {
            yield return _delayBetweenAppearances;
            _cubePool.Issue();
        }
    }
}

public class PoolObject : MonoBehaviour
{
    //private 
}

public class Timer : MonoBehaviour
{
    public Action<PoolObject> LifeTimeIsOver;

    private void Update()
    {
        
    }
}
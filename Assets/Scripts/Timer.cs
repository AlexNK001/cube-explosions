using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAppearances = 0.2f;

    private WaitForSeconds _delayBetweenAppearances;

    public Action TimeHasCome;

    public void Start()
    {
        _delayBetweenAppearances = new(_timeBetweenAppearances);
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (gameObject.activeSelf)
        {
            yield return _delayBetweenAppearances;
            TimeHasCome?.Invoke();
        }
    }
}

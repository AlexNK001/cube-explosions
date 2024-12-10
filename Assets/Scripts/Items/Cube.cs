using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : Item
{
    private ItemPool<Cube> _pool;
    private bool _isContact = false;

    public Action<Cube> TimeLifeIsOver;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isContact == false & collision.collider.TryGetComponent<Platform>(out _))
        {
            _isContact = true;
            StartCoroutine(DelayDisappearance());
            MeshRenderer.material.color = EndingColor;
        }
    }

    public override void Init<T>(ItemPool<T> itemPool)
    {
        _pool = itemPool as ItemPool<Cube>;
    }

    private IEnumerator DelayDisappearance()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(MinDuration, MaxDuration));
        TimeLifeIsOver.Invoke(this);

        _isContact = false;
        ResetMovements();
        MeshRenderer.material.color = StartingColor;
        _pool.Release(this);
    }
}
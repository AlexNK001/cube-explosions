using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : Item
{
    private bool _isContact = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isContact == false & collision.collider.TryGetComponent<Platform>(out _))
        {
            _isContact = true;
            StartCoroutine(DelayDisappearance());
            MeshRenderer.material.color = EndingColor;
        }
    }

    private IEnumerator DelayDisappearance()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(MinDuration, MaxDuration));
        TimeLifeIsOver.Invoke(this);

        _isContact = false;
        MeshRenderer.material.color = StartingColor;
    }
}
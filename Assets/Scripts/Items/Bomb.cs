using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bomb : Item
{
    [SerializeField] private ExplosionCollider _explosionCollider;

    private void OnEnable()
    {
        StartCoroutine(Explode());
        ResetMovements();
    }

    private IEnumerator Explode()
    {
        float currentLifeTime = Random.Range(MinDuration, MaxDuration);
        float lifeTime = currentLifeTime;

        while (currentLifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
            float normalizedCurrentLifeTime = Mathf.InverseLerp(0f, lifeTime, currentLifeTime);
            MeshRenderer.material.color = Color.Lerp(StartingColor, EndingColor, 1f - normalizedCurrentLifeTime);
            yield return null;
        }

        _explosionCollider.gameObject.SetActive(true);
        yield return null;
        _explosionCollider.gameObject.SetActive(false);

        TimeLifeIsOver.Invoke(this);
    }
}

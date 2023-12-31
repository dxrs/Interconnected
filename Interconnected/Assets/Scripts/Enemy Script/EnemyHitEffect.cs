using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] Material hitEffectMat;

    [SerializeField] float durationEffect;

    SpriteRenderer sr;

    Material originalMat;

    Coroutine hitCoroutine;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;
    }

    public void hitEffectEnemy() 
    {
        if (hitCoroutine != null) 
        {
            StopCoroutine(hitEffect());
        }
        hitCoroutine = StartCoroutine(hitEffect());
    }

    IEnumerator hitEffect() 
    {
        sr.material = hitEffectMat;
        yield return new WaitForSeconds(durationEffect);
        sr.material = originalMat;
        hitCoroutine = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : PoolObject
{
    [SerializeField] private Vector3 startPos;
    public event Action<Vector3> OnSpawnEnemy;
    
    private float lerpTime;
    private Vector3 targetPoint;
    private Coroutine translateEffectCoroutine;

    public void Init(Vector3 targetPoint, float lerpTime)
    {
        this.lerpTime = lerpTime;
        this.targetPoint = targetPoint;

        if (translateEffectCoroutine != null)
        {
            StopCoroutine(translateEffectCoroutine);
        }

        translateEffectCoroutine = StartCoroutine(TranslateEffectCoroutine());
    }

    private IEnumerator TranslateEffectCoroutine()
    {
        Vector3 initialPos = targetPoint + startPos;
        transform.position = initialPos;

        float currentTime = 0f;

        while (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, targetPoint, currentTime / lerpTime);
            yield return null;
        }
        
        OnSpawnEnemy?.Invoke(targetPoint);
        OnSpawnEnemy = null;
        
        gameObject.SetActive(false);
    }
}
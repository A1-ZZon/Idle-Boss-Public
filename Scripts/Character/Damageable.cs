using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Color originColor;
    [SerializeField] private Color hitColor;
    [SerializeField] private float hitEffectDuration;

    private WaitForSeconds hitEffectSeconds;

    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor  = skinnedMeshRenderer.material.color;
        hitEffectSeconds = new WaitForSeconds(hitEffectDuration);
    }

    public void OnHit()
    {
        StartCoroutine(HitEffect());    
    }

    IEnumerator HitEffect()
    {
        skinnedMeshRenderer.material.color = hitColor;
        yield return hitEffectSeconds;
        skinnedMeshRenderer.material.color = originColor;
    }
}

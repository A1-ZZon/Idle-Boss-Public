using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    protected Player player;
    protected WaitForSeconds transitionTime;
    public int idx;
    public ParticleSystem EffectParticle;
    public Transform EffectTransform;

    private void Awake()
    {
        player = GetComponentInParent<Player>();        
    }

    public abstract void UseSkill();

    protected abstract List<Enemy> TargetsInRange();    
}
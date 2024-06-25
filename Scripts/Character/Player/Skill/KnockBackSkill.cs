using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackSkill : Skill
{
    public override void UseSkill()
    {
        if (transitionTime == null)
        {
            transitionTime = new WaitForSeconds(3f);
        }
        StartCoroutine(SkillCoroutine());
    }
    
    private IEnumerator SkillCoroutine()
    {
        EffectParticle.Stop();
        EffectParticle.Play();
        yield return transitionTime;
        List<Enemy> targets = TargetsInRange();
        foreach(Enemy target in targets)
        {
            SkillSO data = player.Skill.SkillDatas[idx];
            float damage = (data.AttackData.AtkDamage + player.Stat.AttackDamage) * data.DamageMultiplier;
            player.DamageToTarget(target.Health, damage);            
            Vector3 dir = target.transform.position - player.transform.position;
            dir.y = 0;
            target.KnockBack(1f, target.transform.position + dir.normalized * 30f);
        }
    }

    protected override List<Enemy> TargetsInRange()
    {
        List<Enemy> targets = new List<Enemy>();
        foreach (var target in GameManager.Instance.Stage.SpawnEnemyList)
        {
            Vector3 dir = target.transform.position - player.transform.position;
            dir.y = 0;
            float range = player.Skill.SkillDatas[idx].AttackData.AtkRange;
            if (dir.sqrMagnitude < range * range)
            {
                targets.Add(target);
            }
        }

        return targets;
    }
}
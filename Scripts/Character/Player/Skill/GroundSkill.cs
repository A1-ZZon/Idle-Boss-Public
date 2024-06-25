using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSkill : Skill
{
    public override void UseSkill()
    {
        if (transitionTime == null)
        {
            transitionTime = new WaitForSeconds(1f);
        }
        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        yield return transitionTime;
        List<Enemy> targets = TargetsInRange();
        EffectTransform.position = player.stateMachine.Target.transform.position;
        EffectParticle.Stop();
        EffectParticle.Play();
        foreach (Enemy target in targets)
        {
            SkillSO data = player.Skill.SkillDatas[idx];
            float damage = (data.AttackData.AtkDamage + player.Stat.AttackDamage) * data.DamageMultiplier;
            player.DamageToTarget(target.Health, damage);
            Vector3 dir = target.transform.position - player.stateMachine.Target.transform.position;
            dir.y = 0;
            target.KnockBack(1f, target.transform.position + dir.normalized * 5f);
        }        
    }

    protected override List<Enemy> TargetsInRange()
    {
        List<Enemy> targets = new List<Enemy>();
        foreach (var target in GameManager.Instance.Stage.SpawnEnemyList)
        {
            Vector3 dir = target.transform.position - player.stateMachine.Target.transform.position;
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
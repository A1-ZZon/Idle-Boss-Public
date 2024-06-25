using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionSkill : Skill
{
    public override void UseSkill()
    {
        if (transitionTime == null)
        {
            transitionTime = new WaitForSeconds(1.15f);
        }
        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        AttractionTargets();
        yield return transitionTime;
        
        WaitForSeconds interval = new WaitForSeconds(0.5f);
        for(int i = 0; i < 4; i++)
        {            
            yield return interval;
            List<Enemy> targets = TargetsInRange();
            foreach (Enemy target in targets)
            {
                SkillSO data = player.Skill.SkillDatas[idx];
                float damage = (data.AttackData.AtkDamage + player.Stat.AttackDamage) * data.DamageMultiplier;
                player.DamageToTarget(target.Health, damage);                
            }            
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
            float delta = Mathf.Acos(Vector3.Dot(dir.normalized, player.transform.forward)) * Mathf.Rad2Deg;
            if (dir.sqrMagnitude < range * range && delta < 45)
            {
                targets.Add(target);
            }
        }

        return targets;
    }

    private void AttractionTargets()
    {
        List<Enemy> targets = new List<Enemy>();
        foreach (var target in GameManager.Instance.Stage.SpawnEnemyList)
        {
            Vector3 dist = target.transform.position - player.stateMachine.Target.transform.position;
            dist.y = 0;
            float range = player.Skill.SkillDatas[idx].AttackData.AtkRange;            
            if (dist.sqrMagnitude < range * range)
            {
                Vector3 dir = target.transform.position - player.transform.position;
                dir.y = 0;
                target.KnockBack(1f, player.transform.position + player.transform.forward * 2f);
            }
        }        
    }
}
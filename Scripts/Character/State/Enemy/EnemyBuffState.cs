using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffState : CharacterBaseState
{
    private EnemyStateMachine eStateMachine;
    private Enemy enemy;
    private bool bIsPlayed = false;
    private bool bIsBuffed = false;

    public EnemyBuffState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        eStateMachine = stateMachine as EnemyStateMachine;
        enemy = eStateMachine.Character as Enemy;
    }
    public override void Update()
    {
        base.Update();
        Buff(); 
    }

    void Buff()
    {
        if (!bIsPlayed)
        {
            enemy.Animator.SetTrigger(eStateMachine.Character.AnimationData.BuffParameterHash);
            bIsPlayed = true;
        }
        float nomalizedTime = GetNormalizedTime(enemy.Animator);
        if (!bIsBuffed && nomalizedTime > stateMachine.Data.BaseAtkTransitionTime)
        {
            ApplyBuff();
            bIsBuffed = true;
        }
        if(nomalizedTime >= 1)
        {
            bIsPlayed = false;
            bIsBuffed = false;
            eStateMachine.ChangeState(eStateMachine.IdleState);
        }
    }

    void ApplyBuff()
    {
        List<Enemy> enemyList = GameManager.Instance.Stage.SpawnEnemyList;

        BuffEffect buffAreaEffect = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.BuffAreaEffect).ReturnMyComponent<BuffEffect>();
        buffAreaEffect.SetPosition(enemy);

        foreach (Enemy em in enemyList)
        {
            em.Health.Heal(enemy.Stat.HealAmount);
            enemy.Buff.IncreaseAttackPower(em, enemy.Stat.IncreaseAttackPowerAmount);

            BuffEffect buffEffect = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.BuffEffect).ReturnMyComponent<BuffEffect>();
            buffEffect.SetPosition(em);
        }
        enemy.Buff.AlreadyBuffed();
    }

    float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!animator.IsInTransition(0))
        {
            float result = currentInfo.normalizedTime;
            return result;
        }
        else
        {
            return 0f;
        }
    }
}
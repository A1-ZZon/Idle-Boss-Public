using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    private bool alreadyAppliedDealing;

    public CharacterAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();         
    }

    protected float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
                
        if (!animator.IsInTransition(0))
        {
            float result = currentInfo.normalizedTime;
            result %= 1f;
            return result;
        }
        else
        {
            return 0f;
        }
    }

    protected void Attack(float normalizedTime)
    {        
        if (normalizedTime < 0.95f && normalizedTime > stateMachine.Data.BaseAtkTransitionTime)
        {
            if (!alreadyAppliedDealing)
            {
                alreadyAppliedDealing = true;
                float rand = Random.Range(0f, 1f);
                bool crit = rand < stateMachine.Stat.CriticalRate;
                float criticalMultiplier = crit ? stateMachine.Stat.CriticalMultiplier : 1f;
                stateMachine.Target.TakeDamage(stateMachine.Stat.AttackDamage * criticalMultiplier, crit);
            }
        }
        else
        {
            alreadyAppliedDealing = false;
        }
    }
}

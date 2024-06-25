using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState : IState
{
    protected CharacterStateMachine stateMachine;    

    public CharacterBaseState(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
  
    }

    public virtual void Update()
    {
        if (stateMachine.Character.Health.IsDie) return;
    }

    public virtual void PhysicsUpdate()
    {

    }
    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Character.Animator.SetBool(animatorHash, true);
    }

    protected void StartAnimation(int animatorHash, int idx)
    {
        stateMachine.Character.Animator.SetInteger(animatorHash, idx);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Character.Animator.SetBool(animatorHash, false);
    }

    protected bool IsInAttackRange()
    {
        Vector3 pos = (stateMachine.Target.transform.position - stateMachine.Character.transform.position);
        pos.y = 0;
        float playerDistanceSqr = pos.sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Data.BaseAttackData.AtkRange * stateMachine.Data.BaseAttackData.AtkRange;
    }
}

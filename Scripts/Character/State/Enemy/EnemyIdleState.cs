using UnityEngine;

public class EnemyIdleState : CharacterIdleState
{
    private EnemyStateMachine eStateMachine;
    private Enemy enemy;
    public EnemyIdleState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        eStateMachine = stateMachine as EnemyStateMachine;
        enemy = eStateMachine.Character as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(eStateMachine.Character.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(eStateMachine.Character.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        if(!GameManager.Instance.Stage.isStageReady) return;
        
        base.Update();

        if (IsPlayerAlive())
        {
            if(IsInAttackRange()) eStateMachine.ChangeState(eStateMachine.AttackState);
            else eStateMachine.ChangeState(eStateMachine.MoveState);
        }
    }

    bool IsPlayerAlive()
    {
        return !GameManager.Instance.Player.Health.IsDie;
    }
}
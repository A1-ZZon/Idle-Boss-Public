using UnityEngine;

public class EnemyMoveState : CharacterMoveState
{
    private EnemyStateMachine eStateMachine;
    private Enemy enemy;
    public EnemyMoveState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        eStateMachine = stateMachine as EnemyStateMachine;
        enemy = stateMachine.Character as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(eStateMachine.Character.AnimationData.MoveParameterHash);       
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(eStateMachine.Character.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Character.Health.IsDie) return;

        if (eStateMachine.Target.IsDie)
        {
            stateMachine.Target = null;
            eStateMachine.ChangeState(eStateMachine.IdleState);
            return;
        }

        if (!IsInAttackRange() && enemy.Controller.enabled) Move();
        else
        {
            Stop();
            eStateMachine.ChangeState(eStateMachine.AttackState);
        }
    }

    protected override void MoveToTarget(Vector3 movementDirection)
    {
        base.MoveToTarget(movementDirection);
        if (stateMachine.Character is Enemy && eStateMachine.Character.Controller!= null)
        {
            Enemy em = stateMachine.Character as Enemy;
            eStateMachine.Character.Controller.Move(movementDirection * em.Stat.MoveSpeed * Time.deltaTime);
        }
    }

    void Stop()
    {
        if (enemy.Controller.enabled) eStateMachine.Character.Controller.Move(Vector3.zero);
    }
}
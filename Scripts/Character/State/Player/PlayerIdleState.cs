using UnityEngine;

public class PlayerIdleState : CharacterIdleState
{
    PlayerStateMachine stateMachine;
    public PlayerIdleState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Character.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Character.AnimationData.IdleParameterHash);
    } 

    public override void Update()
    {
        if(!GameManager.Instance.Stage.isStageReady) return;      
        base.Update();
        if (!stateMachine.CheckEqualState(this)) return;
        if (stateMachine.CheckTargetExit())
        {
            if (IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.MoveState);
                return;
            }            
        }
        else
        {
            if(!CheckLocateStartPos())
            {
                stateMachine.ChangeState(stateMachine.MoveState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.AirState);
                return;
            }            
        }
    }

    private bool CheckLocateStartPos()
    {
        Player player = stateMachine.Character as Player;
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;
        Vector3 startPos = player.StartPos;
        startPos.y = 0;
        if ((playerPos - startPos).sqrMagnitude < 0.01f) return true;
        return false;
    }
}
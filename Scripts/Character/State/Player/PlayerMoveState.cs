using UnityEngine;

public class PlayerMoveState : CharacterMoveState
{
    PlayerStateMachine stateMachine;
    public PlayerMoveState(CharacterStateMachine stateMachine) : base(stateMachine)
    {        
        this.stateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Character.AnimationData.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Character.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.CheckEqualState(this)) return;
        if (stateMachine.CheckTargetExit()) // 적이 존재 시
        {
            if (IsInAttackRange() && !stateMachine.Target.IsDie) // 적이 사거리 안에 존재 시
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
            Move(); 
        }
        else
        {
            if (CheckLocateStartPos()) // 시작위치에 도착했을 시
            {
                stateMachine.Character.transform.rotation = Quaternion.identity;
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
            MoveToStartPos(); // 시작위치로 이동
        }
    }

    protected override void MoveToTarget(Vector3 movementDirection)
    {
        Vector3 dir = (movementDirection.normalized * stateMachine.Data.BaseMoveSpeed);
        stateMachine.Character.Controller.Move(dir * Time.deltaTime);
    }

    private void MoveToStartPos()
    {
        Player player = stateMachine.Character as Player;
        Vector3 dir = player.StartPos - player.transform.position;
        dir.y = 0;
        Rotate(dir);
        MoveToward();
    }

    private void MoveToward()
    {       
        Player player = stateMachine.Character as Player;
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;
        Vector3 startPos = player.StartPos;
        startPos.y = 0;
        Vector3 pos = Vector3.MoveTowards(playerPos, startPos, stateMachine.Data.BaseMoveSpeed * Time.deltaTime);        
        stateMachine.Character.Controller.Move(pos - playerPos);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : CharacterStateMachine
{   
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerSkillState SkillState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Character = player;
        Data = player.Data as PlayerSO;
        Stat = player.Stat;

        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        AttackState = new PlayerAttackState(this);
        AirState = new PlayerAirState(this);
        SkillState = new PlayerSkillState(this);
        //MovementSpeed = player.Data.RunSpeed;
    }

    public bool CheckTargetExit()
    {
        if (Target != null && !Target.IsDie) return true; // 이미 타겟이 있으면
        List<Enemy> targets = GameManager.Instance.Stage.SpawnEnemyList;
        if(targets.Count == 0) return false; // 적이 0마리면
        float minDistance = float.MaxValue;
        foreach (var target in targets)
        {
            Vector3 dir = (target.transform.position - Character.transform.position);
            dir.y = 0;
            float dist = dir.magnitude;
            if (dist < minDistance)
            {
                minDistance = dist;
                Target = target.gameObject.GetComponent<Health>();
            }
        }

        return true;
    }
}

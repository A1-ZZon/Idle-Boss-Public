using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMoveState : CharacterBaseState
{    
    public CharacterMoveState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // TODO : character Initialize
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();        
    }

    protected void Move()
    {
        Vector3 movementDirection = GetDirectionToTarget();
        Rotate(movementDirection);
        MoveToTarget(movementDirection);        
    }

    protected virtual void MoveToTarget(Vector3 movementDirection)
    {
        // TODO CharacterController.Move 구현
    }

    protected virtual void Rotate(Vector3 movementDirection)
    {
        stateMachine.Character.transform.rotation = Quaternion.LookRotation(movementDirection);
    }

    protected virtual Vector3 GetDirectionToTarget()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Character.transform.position).normalized;
        dir.y = 0;
        return dir;       
    }    

    
}

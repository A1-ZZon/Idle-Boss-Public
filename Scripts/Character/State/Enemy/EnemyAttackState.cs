using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : CharacterAttackState
{
    private EnemyStateMachine eStateMachine;
    private Enemy enemy;

    private bool alreadyAppliedDealing = false;
    private float nomalizedTime;

    public EnemyAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        eStateMachine = stateMachine as EnemyStateMachine;
        enemy = stateMachine.Character as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(eStateMachine.Character.AnimationData.AttackParameterHash);
        stateMachine.Character.Animator.SetFloat(enemy.AnimationData.AttackSpeedParameterHash, enemy.Stat.AttackSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(eStateMachine.Character.AnimationData.AttackParameterHash);
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
        if (enemy.Buff != null)
        {
            if (!enemy.Buff.IsAlreadyBuffed)
            {
                stateMachine.ChangeState(eStateMachine.BuffState);
                return ;
            }
        }
        if (!IsInAttackRange())
        {
            eStateMachine.ChangeState(eStateMachine.MoveState);
            return;
        }

        Rotate();

        nomalizedTime = GetNormalizedTime(eStateMachine.Character.Animator);
        CheckEnemyAttackType(enemy.Stat.EnemyType);
    }

    void CheckEnemyAttackType(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Melee:
                MeleeAttack();
                break;
            case EnemyType.Ranged:
            case EnemyType.Buffer:
                RangedAttack();
                break;
        }
    }

    void MeleeAttack()
    {
        Attack(nomalizedTime);
    }

    void RangedAttack()
    {
        if (nomalizedTime < 0.95f && nomalizedTime > stateMachine.Data.BaseAtkTransitionTime)
        {
            if (!alreadyAppliedDealing)
            {
                alreadyAppliedDealing = true;
                Shoot();
            }
        }
        else alreadyAppliedDealing = false;
    }

    void Shoot()
    {
        RangedEnemy rangedEnemy = enemy as RangedEnemy;
        // 총알 생성
        Bullet bullet = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.EnemyBullet).ReturnMyComponent<Bullet>();
        bullet.transform.position = rangedEnemy.bulletSpawnPos.position;
        bullet.OnHitBullet += AttackTarget;
    }

    void AttackTarget()
    {
        if (stateMachine.Target != null)
        {
            float rand = Random.Range(0f, 1f);
            bool crit = rand < enemy.Stat.CriticalRate;
            float criticalMultiplier = crit ? enemy.Stat.CriticalMultiplier : 1f;
            stateMachine.Target.TakeDamage(enemy.Stat.AttackDamage * criticalMultiplier, crit);
        }
    }

    void Rotate()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Character.transform.position).normalized;
        dir.y = 0;
        stateMachine.Character.transform.rotation = Quaternion.LookRotation(dir);
    }
}

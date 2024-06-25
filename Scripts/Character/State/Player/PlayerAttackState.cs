
public class PlayerAttackState : CharacterAttackState
{
    PlayerStateMachine stateMachine;
    Player player;
    public PlayerAttackState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine as PlayerStateMachine;
        player = this.stateMachine.Character as Player;
    }

    public override void Enter()
    {
        base.Enter();
        Player player = stateMachine.Character as Player;        
        StartAnimation(stateMachine.Character.AnimationData.AttackParameterHash);
        stateMachine.Character.Animator.SetFloat(player.AnimationData.AttackSpeedParameterHash, player.Stat.AttackSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Character.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.CheckEqualState(this)) return;
        if (!stateMachine.Character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return;
        float normalizedTime = GetNormalizedTime(stateMachine.Character.Animator);
        if (stateMachine.Target.IsDie && normalizedTime > 0.95f)
        {
            stateMachine.Target = null;
            stateMachine.ChangeState(stateMachine.MoveState);
            return; 
        }
        else
        {
            Attack(normalizedTime);
        }        
    }
}
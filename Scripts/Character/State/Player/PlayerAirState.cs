using UnityEngine;

public class PlayerAirState : CharacterBaseState
{
    PlayerStateMachine stateMachine;
    private bool isLanding = false;    

    public PlayerAirState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine as PlayerStateMachine;
        GameManager.Instance.Stage.CubeRotationEvent.OnRotatinEnd += LandPlayer;
    }

    public override void Enter()
    {
        base.Enter();   
        isLanding = false;        
        StartAnimation(stateMachine.Character.AnimationData.AirParameterHash);
        stateMachine.Character.Health.InitHealth(stateMachine.Stat.MaxHealth);
        GameManager.Instance.Stage.StageClear();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Character.AnimationData.AirParameterHash);
    }

    public override void Update()
    {
        base.Update();
        // if (!isLanding) // TODO : StageReady 정보 필요
        // {
        //     
        //     isLanding = true;
        //     stateMachine.character.Animator.SetTrigger(stateMachine.character.AnimationData.LandingParameterHash);
        //     GameManager.Instance.Stage.StageClear();
        // }
        // else
        // {
        //     if (stateMachine.character.Animator.IsInTransition(0)) return;
        //     AnimatorStateInfo currentInfo = stateMachine.character.Animator.GetCurrentAnimatorStateInfo(0);            
        //     if (currentInfo.normalizedTime < 1f) return;          
        //     stateMachine.ChangeState(stateMachine.IdleState);
        //     return;
        // }

        if (isLanding)
        {
            AnimatorStateInfo currentInfo = stateMachine.Character.Animator.GetCurrentAnimatorStateInfo(0);    
            if (currentInfo.normalizedTime < 1f) return;   
            GameManager.Instance.Stage.StartNewStage();        
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        
    }

    private void LandPlayer()
    {
        isLanding = true;
        stateMachine.Character.Animator.SetTrigger(stateMachine.Character.AnimationData.LandingParameterHash);
    }
}
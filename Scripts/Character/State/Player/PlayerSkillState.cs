using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : CharacterBaseState
{
    PlayerStateMachine stateMachine;
    Player player;
    public PlayerSkillState(CharacterStateMachine stateMachine) : base(stateMachine)
    {
        player = stateMachine.Character as Player;
        this.stateMachine = stateMachine as PlayerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //player.Skill.AutoSelectSkill();        
        StartAnimation(stateMachine.Character.AnimationData.SkillParameterHash);
        player.Animator.SetTrigger(stateMachine.Character.AnimationData.SkillTriggerParameterHash);
        StartAnimation(player.AnimationData.SkillIdxParameterHash, player.Skill.SkillIdx);
        player.Skill.StartSkillAnimation();
        player.Skill.UseSkill();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Character.AnimationData.SkillParameterHash);        
    }

    public override void Update()
    {
        base.Update();

        AnimatorStateInfo currentInfo = stateMachine.Character.Animator.GetCurrentAnimatorStateInfo(0);
        
        if(currentInfo.IsTag("Skill"))
        {
            
        }
        if (currentInfo.normalizedTime >= 1f && currentInfo.IsTag("Skill"))
        {
            player.Skill.SkillDone();
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }        
    }
}

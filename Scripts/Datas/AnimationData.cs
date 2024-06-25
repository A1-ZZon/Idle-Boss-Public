using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string moveParameterName = "Move";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string buffParameterName = "Buff";
    [SerializeField] private string dieParameterName = "Die";
    [SerializeField] private string attackSpeedParameterName = "AttackSpeed";
    [SerializeField] private string airParameterName = "Air";
    [SerializeField] private string landingParameterName = "Landing";
    [SerializeField] private string skillParameterName = "@Skill";
    [SerializeField] private string skillIdxParameterName = "SkillIdx";
    [SerializeField] private string skillTriggerParameterName = "SkillTrigger";


    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BuffParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }
    public int AttackSpeedParameterHash { get; private set; }
    public int AirParameterHash { get; private set; }  
    public int LandingParameterHash { get; private set; }
    public int SkillParameterHash { get; private set; }
    public int SkillIdxParameterHash { get; private set; }
    public int SkillTriggerParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        BuffParameterHash = Animator.StringToHash(buffParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
        AttackSpeedParameterHash = Animator.StringToHash(attackSpeedParameterName);
        AirParameterHash = Animator.StringToHash(airParameterName); 
        LandingParameterHash = Animator.StringToHash(landingParameterName);
        SkillParameterHash = Animator.StringToHash(skillParameterName);
        SkillIdxParameterHash = Animator.StringToHash(skillIdxParameterName);
        SkillTriggerParameterHash = Animator.StringToHash(skillTriggerParameterName);
    }
}
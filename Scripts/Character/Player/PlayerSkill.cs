using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{    
    public int SkillIdx { get; private set; }
    public bool IsCastingSkill { get; private set; }    
    public List<float> CurSkillCoolDown { get; private set; }    
    [field: SerializeField] public List<SkillSO> SkillDatas { get; private set; }
    [field: SerializeField] public List<Skill> Skills { get; private set; }

    private float CurSkillDelay = 0f;

    public event Action<int, float> OnKillActive;

    private void Awake()
    {
        CurSkillCoolDown = new List<float>();
        IsCastingSkill = false;
        for (int i = 0; i < SkillDatas.Count; i++)
        {
            CurSkillCoolDown.Add(SkillDatas[i].CoolDown);
            Skills[i].idx = i;
        }
        //SkillIdx = -1;
    }

    private void Update()
    {
        for(int i = 0; i < CurSkillCoolDown.Count; i++)
        {
            CurSkillCoolDown[i] -= Time.deltaTime;
            CurSkillCoolDown[i] = Mathf.Max(0, CurSkillCoolDown[i]);
        }
        CurSkillDelay -= Time.deltaTime;
        CurSkillDelay = Mathf.Max(0, CurSkillDelay);
    }

    private void CallActiveSkill(int idx, float coolTime)
    {
        OnKillActive?.Invoke(idx, coolTime);
    }

    public bool CanUseSkill()
    {        
        if (IsCastingSkill || CurSkillDelay != 0f) return false;
        
        for (int i = 0; i < SkillDatas.Count; i++)
        {
            if(CheckTargetInRange(i) && CurSkillCoolDown[i] == 0f)
            {
                SkillIdx = i;
                return true;
            }
        }
        return false;
    }    

    public void AutoSelectSkill()
    {
        for (int i = 0; i < CurSkillCoolDown.Count; i++)
        {
            if (CurSkillCoolDown[i] == 0f)
            {
                SkillIdx = i;
                break;
            }
        }
    }

    private bool CheckTargetInRange(int idx)
    {
        float range = SkillDatas[idx].DetectedRange;
        if (GameManager.Instance.Stage.SpawnEnemyList == null) return false;
        foreach(var target in GameManager.Instance.Stage.SpawnEnemyList)
        {
            Vector3 dir = target.transform.position - gameObject.transform.position;
            dir.y = 0;
            if (dir.sqrMagnitude <= range * range) return true;
        }

        return false;
    }

    public void StartSkillAnimation()
    {
        IsCastingSkill = true;
    }

    public void UseSkill()
    {        
        IsCastingSkill = true;
        CurSkillCoolDown[SkillIdx] = SkillDatas[SkillIdx].CoolDown;
        Skills[SkillIdx].UseSkill();
        CallActiveSkill(SkillIdx, SkillDatas[SkillIdx].CoolDown);
        Debug.Log($"Skill{SkillIdx} Casting");
    }

    public void SkillDone()
    {
        //SkillIdx = -1;
        IsCastingSkill = false;
        CurSkillDelay = 3f;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SkillMenuUI : MonoBehaviour
{
    [SerializeField] private List<SkillUI> skillUI;

    private void Start()
    {
        GameManager.Instance.Player.Skill.OnKillActive += Skill_OnKillActive;
    }

    private void Skill_OnKillActive(int skillIdx, float coolTime)
    {
        skillUI[skillIdx].ActiveCoolDown(coolTime);
    }
}

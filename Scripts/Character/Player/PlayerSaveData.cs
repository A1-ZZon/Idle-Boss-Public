using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

[Serializable]
public class PlayerSaveData
{
    [field: SerializeField] public int StageLevel { get; private set; }
    [field: SerializeField] public int Gold { get; private set; }
    [field: SerializeField] public int DamageLevel { get; private set; }
    [field: SerializeField] public int AttackSpeedLevel { get; private set; }
    [field: SerializeField] public int CriticalRateLevel { get; private set; }
    [field: SerializeField] public int CriticalMultiplierLevel { get; private set; }
    [field: SerializeField] public int HealthLevel { get; private set; }

    public PlayerSaveData(int stageLevel, int gold, int damageLevel, int attackSpeedLevel, int criticalRateLevel, int criticalMultiplierLevel, int healthLevel)
    {
        StageLevel = stageLevel;
        Gold = gold;
        DamageLevel = damageLevel;
        AttackSpeedLevel = attackSpeedLevel;
        CriticalRateLevel = criticalRateLevel;
        CriticalMultiplierLevel = criticalMultiplierLevel;
        HealthLevel = healthLevel;
    }

    public List<int> GetLevelList()
    {
        List<int> levels = new()
        {
            DamageLevel,
            AttackSpeedLevel,
            CriticalRateLevel,
            CriticalMultiplierLevel,
            HealthLevel
        };
        return levels;
    }

    public void ChangeGold(int amount)
    {
        Gold += amount;
    }

    public void SetStageLevel(int level)
    {
        StageLevel = level;
    }

    public void OnLevelUpStat(EEnhancementType type)
    {
        switch (type)
        {
            case EEnhancementType.DAMAGE:
                DamageLevel++;
                break;
            case EEnhancementType.ATTACKSPEED:
                AttackSpeedLevel++;
                break;
            case EEnhancementType.CRITRATE:
                CriticalRateLevel++;
                break;
            case EEnhancementType.CRITMULTIPLIER:
                CriticalMultiplierLevel++;
                break;
            case EEnhancementType.HEALTH:
                HealthLevel++;
                break;
        }
    }
}

using System;
using UnityEngine;

[Serializable]
public class EnemyStat : CharacterStat
{
    private readonly EnemySO data;
    private EnemyStatLevelUpSO levelUpSo;
    private int enforceLevel;

    [Header("Type")]
    public EnemyType EnemyType;

    public float AttackRange;
    [Range(1f, 3f)] public float AttackTransitionTime;

    [Header("Drop Items")]
    public int Gold;

    [Header("Buff Values")]
    public int HealAmount;
    public int IncreaseAttackPowerAmount;

    public EnemyStat(EnemySO data, EnemyStatLevelUpSO levelUpSo)
    {
        this.data = data;
        this.levelUpSo = levelUpSo;
        enforceLevel = GameManager.Instance.Stage.EnemyEnforceLevel;
        Initialize();
    }

    public void Initialize()
    {
        EnemyType = data.EnemyType;
        // 강화 적용
        MaxHealth = data.MaxHealth + (data.MaxHealth * (levelUpSo.IncreaseMaxHealth * enforceLevel ));
        MoveSpeed = data.BaseMoveSpeed+ (data.BaseMoveSpeed * (levelUpSo.IncreaseMoveSpeed * enforceLevel ));
        AttackSpeed = data.BaseAtkSpeed +  (data.BaseAtkSpeed * (levelUpSo.IncreaseBaseAtkSpeed * enforceLevel )) ;
        AttackDamage = data.BaseAttackData.AtkDamage + (data.BaseAttackData.AtkDamage * (levelUpSo.IncreaseBaseAttackDamage * enforceLevel ));
        CriticalRate = data.BaseCriticalRate + (data.BaseCriticalRate* (levelUpSo.IncreaseBaseCriticalRate * enforceLevel ));
        CriticalMultiplier = data.BaseCriticalMultiplier + (data.BaseCriticalMultiplier * (levelUpSo.IncreaseBaseCriticalMultiplier * enforceLevel ));
        Gold = data.Gold + (data.Gold * (levelUpSo.IncreaseBaseGold * enforceLevel));
        HealAmount = data.HealAmount+data.HealAmount * (levelUpSo.IncreaseBaseHealAmount * enforceLevel);
        IncreaseAttackPowerAmount = data.IncreaseAttackPowerAmount+ data.IncreaseAttackPowerAmount * (levelUpSo.IncreaseBaseIncreaseAttackPowerAmount * enforceLevel);

        AttackTransitionTime = data.BaseAtkTransitionTime;
        AttackRange = data.BaseAttackData.AtkRange ;
    }
}
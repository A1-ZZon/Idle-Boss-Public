using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatLevelUpSO : ScriptableObject
{
    [field : Header("Move")]
    [field: SerializeField] public float IncreaseMoveSpeed { get; private set; }

    [field: Header("Health")]
    [field: SerializeField] public float IncreaseMaxHealth { get; private set; }

    [field: Header("BaseAttack")]
    [field: SerializeField]public float IncreaseBaseAtkSpeed { get; private set; }
    [field: SerializeField] public float IncreaseBaseAttackDamage{ get; private set; }

    [field: Header("BaseCritical")]
    [field: SerializeField] public float IncreaseBaseCriticalRate { get; private set; }
    [field: SerializeField] public float IncreaseBaseCriticalMultiplier { get; private set; }
}

[CreateAssetMenu(fileName = "EnemyStatLevelUp", menuName = "CharacterStatLevelUp/EnemyStatLevelUp")]
public class EnemyStatLevelUpSO : CharacterStatLevelUpSO
{
    [field: Header("Drop Items")]
    [field: SerializeField] public int IncreaseBaseGold { get; private set; }

    [field: Header("Buff Values")]
    [field: SerializeField] public int IncreaseBaseHealAmount { get; private set; }
    [field: SerializeField] public int IncreaseBaseIncreaseAttackPowerAmount { get; private set; }
}
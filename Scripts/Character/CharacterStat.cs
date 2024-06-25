using UnityEngine;

public class CharacterStat
{
    [Header("Health")]
    public float MaxHealth;

    [Header("Move")]
    public float MoveSpeed;

    [Header("Attack")]
    [Range(1f, 3f)] public float AttackSpeed;
    public float AttackDamage;

    [Header("Critical")]
    [Range(0f, 1f)] public float CriticalRate;
    [Min(1f)] public float CriticalMultiplier;
}
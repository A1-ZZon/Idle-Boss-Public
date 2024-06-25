using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]

public class EnemySO : CharacterSO
{
    [field: Header("Enemy Type")]
    public EnemyType EnemyType;

    [field: Header("DropItems")]
    public int Gold;
    public List<GameObject> DropItems;

    [field: Header("Buff Values")]
    public int HealAmount;
    public int IncreaseAttackPowerAmount;
}

public enum EnemyType
{
    Melee,
    Ranged,
    Buffer
}
using System;
using UnityEngine;

public class CharacterSO : ScriptableObject
{
    [field : Header("Move")]
    [field: SerializeField] public float BaseMoveSpeed { get; private set; }

    [field: Header("Health")]
    [field: SerializeField] public float MaxHealth { get; private set; }

    [field: Header("BaseAttack")]
    [field: SerializeField][field: Range(0.5f, 3f)] public float BaseAtkSpeed { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float BaseAtkTransitionTime { get; private set; }
    [field: SerializeField] public BaseAttackData BaseAttackData { get; private set; }

    [field: Header("BaseCritical")]
    [field: SerializeField][field: Range(0f, 1f)] public float BaseCriticalRate { get; private set; }
    [field: SerializeField][field: Min(1f)] public float BaseCriticalMultiplier { get; private set; }
}

[Serializable]
public class BaseAttackData
{       
    [field: SerializeField] public float AtkRange { get; private set; } 
    [field: SerializeField] public float AtkDamage { get; private set; }  
}
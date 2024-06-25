using System;
using System.Collections.Generic;
using UnityEngine;

public enum EEnhancementType
{
    DAMAGE,
    ATTACKSPEED,
    CRITRATE,
    CRITMULTIPLIER,
    HEALTH,
    COUNT
}

[Serializable]
public class PlayerStat : CharacterStat
{   
    public PlayerStat(float damage, float atkSpeed, float critRate, float critMultiplier, float health)
    {
        AttackDamage = damage;
        AttackSpeed = atkSpeed;
        CriticalRate = critRate;
        CriticalMultiplier = critMultiplier;
        MaxHealth = health;
    }       
    
    public void UpdateStat(EEnhancementType type, float value)
    {
        switch(type)
        {
            case EEnhancementType.DAMAGE:
                AttackDamage = value; 
                break;
            case EEnhancementType.ATTACKSPEED:
                AttackSpeed = value; 
                break;
            case EEnhancementType.CRITRATE:
                CriticalRate = value;  
                break;
            case EEnhancementType.CRITMULTIPLIER:
                CriticalMultiplier = value; 
                break;
            case EEnhancementType.HEALTH:
                MaxHealth = value; 
                break;
        }
    }

    public List<float> GetStatList()
    {
        List<float> levels = new()
        {
            AttackDamage,
            AttackSpeed,
            CriticalRate,
            CriticalMultiplier,
            MaxHealth
        };
        return levels;
    }
}

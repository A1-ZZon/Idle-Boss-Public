using System;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementInfo
{
    public string Name { get; private set; }
    public float Increasement { get; private set; }
    public int Payment { get; private set; }
}


[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]

public class PlayerSO : CharacterSO
{
    [field: Header("Increament")]
    [field: SerializeField] public float DamageIncreament { get; private set; }
    [field: SerializeField] public float AttackSpeedIncreament { get; private set; }
    [field: SerializeField] public float CriticalRateIncreament { get; private set; }
    [field: SerializeField] public float CriticalMultiplierIncreament { get; private set; }
    [field: SerializeField] public float HealthIncreament { get; private set; }    
}


using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    [field: Header("Info")]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public BaseAttackData AttackData { get; private set; }
    [field: SerializeField] public float TransitionTime { get; private set; }
    [field: SerializeField] public float DetectedRange { get; private set; }
    [field: SerializeField] public float DamageMultiplier { get; private set; }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;




public class Player : Character
{    
    [field: SerializeField] public PlayerStat Stat { get; private set; }
    public PlayerSaveData SaveData { get; private set; }
    public PlayerSkill Skill { get; private set; }
    public Vector3 StartPos { get; private set; }

    public PlayerStateMachine stateMachine;

    public event Action OnChangeGold;
    public event Action<EEnhancementType> OnStatLevelUp;

    protected override void Awake()
    {
        base.Awake();
        Skill = GetComponent<PlayerSkill>();
        Stat = new PlayerStat(Data.BaseAttackData.AtkDamage, Data.BaseAtkSpeed, Data.BaseCriticalRate, Data.BaseCriticalMultiplier, Data.MaxHealth);
        // TODO : LoadJson
        Debug.Log("dd");
        GameManager.Instance.Player = this;
        StartPos = gameObject.transform.position;        
    }

    private void Start()
    {        
        Debug.Log("daaa");
        SaveData = GameManager.Instance.PlayerData.DataLoad();
        GameManager.Instance.Stage.SetStageLevel(SaveData.StageLevel);
        Init();
        Health.InitHealth(Stat.MaxHealth);
        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnPlayerDamageText += CallPlayerDamageText;
    }

    private void CallPlayerDamageText(int damage, bool isCrit)
    {
        UIManager.Instance.CallPlayerDamageUI(damage, isCrit);
    }

    private void Update()
    {   
        stateMachine.Update();
        if (GameManager.Instance.Stage.isStageReady && Skill.CanUseSkill())
        {
            stateMachine.ChangeState(stateMachine.SkillState);
        }
    }

    public void Init()
    {        
        for(int i = 0; i < (int)EEnhancementType.COUNT; i++)
        {
            ChangeStat((EEnhancementType)i);
        }
    }

    private void ChangeStat(EEnhancementType type)
    {
        float value = 0f;
        float increament = GetIncreamentList()[(int)type];
        int level = SaveData.GetLevelList()[(int)type];
        float amount = increament * level;
        switch (type)
        {
            case EEnhancementType.DAMAGE:
                value = Data.BaseAttackData.AtkDamage;
                value += amount;
                break;
            case EEnhancementType.ATTACKSPEED:
                value = Data.BaseAtkSpeed;
                value += amount;
                break;
            case EEnhancementType.CRITRATE:
                value = Data.BaseCriticalRate;
                value += amount;
                break;
            case EEnhancementType.CRITMULTIPLIER:
                value = Data.BaseCriticalMultiplier;
                value += amount;
                break;
            case EEnhancementType.HEALTH:
                value = Data.MaxHealth;
                value += amount;
                Health.ChangeMaxHealth(increament);
                break;
        }        
        
        Stat.UpdateStat(type, value);
    }

    public List<float> GetIncreamentList()
    {
        PlayerSO data = (PlayerSO)Data;
        List<float> list = new()
        {
            data.DamageIncreament,
            data.AttackSpeedIncreament,
            data.CriticalRateIncreament,
            data.CriticalMultiplierIncreament,
            data.HealthIncreament
        };
        return list;
    }

    public void CallChangeGold(int amount)
    {
        // if (SaveData.Gold + amount < 0) return false;
        SaveData.ChangeGold(amount);
        OnChangeGold?.Invoke();        
    }

    public void CallLevelUpEvent(int type)
    {
        SaveData.OnLevelUpStat((EEnhancementType)type);
        ChangeStat((EEnhancementType)type);
        OnStatLevelUp?.Invoke((EEnhancementType)type);
    }    

    public void DamageToTarget(Health target, float damage)
    {
        float rand = UnityEngine.Random.Range(0f, 1f);
        bool crit = rand < Stat.CriticalRate;
        float multiplier = crit ? Stat.CriticalMultiplier : 1f;
        target.TakeDamage(damage * multiplier, crit);
    }

}

using System;
using UnityEngine;

public class Enemy : Character
{
    [field: SerializeField] public EnemyStat Stat { get; private set; }
    public EnemyBuff Buff { get; private set; }

    private EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        Buff = GetComponent<EnemyBuff>();
    }

    private void Start()
    {
        stateMachine.Target = GameManager.Instance.Player.GetComponent<Health>();
        
    }

    private void CallDamageText(int damage, bool isCrit, Transform transform)
    {
        UIManager.Instance.CallEnemyDamageUI(damage, isCrit, transform);
    }

    protected override void OnEnable()
    {
        Stat = new EnemyStat((EnemySO)Data, GameManager.Instance.Stage.EnemyStatLevelUpData);
        if (stateMachine == null)
        {
            stateMachine = new EnemyStateMachine(this);
        }
        
        base.OnEnable();
        Health.OnDie += EnemyDie;
        Health.OnEnemyDamageText += CallDamageText;
        Init();
    }

    private void Update()
    {
        stateMachine.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Health.OnDie -= EnemyDie;
        Health.OnEnemyDamageText -= CallDamageText;
    }

    void Init()
    {
        Health.IsDie = false;
        Controller.enabled = true;
        Health.InitHealth(Stat.MaxHealth);
        stateMachine.ChangeState(stateMachine.IdleState);
        
    }

    void EnemyDie()
    {
        GameManager.Instance.Stage.SpawnEnemyList.Remove(this);
    }
}
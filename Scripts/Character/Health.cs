using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    public bool IsDie = false;

    private Damageable damageable;
    
    public event Action OnDie;
    public event Action OnChangeHealth;
    public event Action<int, bool, Transform> OnEnemyDamageText;
    public event Action<int, bool> OnPlayerDamageText;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        health = maxHealth;
        IsDie = false;
    }

    public void CallEnemyDamageText(int damage, bool isCrit, Transform transform)
    {
        OnEnemyDamageText?.Invoke(damage, isCrit, transform);
    }

    public void CallPlayerDamageText(int damage, bool isCrit)
    {
        OnPlayerDamageText?.Invoke(damage, isCrit);
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        if (health == 0) return;
        float rand = UnityEngine.Random.Range(0.85f, 1.15f);
        float dmg = rand * damage;
        damageable.OnHit();
        health = Mathf.Max(health - dmg, 0);
        OnChangeHealth?.Invoke();

        if (health == 0)
        {
            IsDie = true;  
            OnDie?.Invoke();
        }

        CallEnemyDamageText((int)dmg, isCrit, transform);
        CallPlayerDamageText((int)dmg, isCrit);
        //Debug.Log(damage);
    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        OnChangeHealth?.Invoke();
    }

    public void InitHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = this.maxHealth;
        OnChangeHealth?.Invoke();
    }

    public void ChangeMaxHealth(float amount)
    {
        this.maxHealth += amount;
        health += amount;
        OnChangeHealth?.Invoke();
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }

    public int GetMaxHealth() 
    {
        return (int)maxHealth;
    }

    public int GetCurHealth()
    {
        return (int)health;
    }
}
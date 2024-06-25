using System.Collections;
using UnityEngine;

public class EnemyBuff : MonoBehaviour
{
    [SerializeField] private float buffCoolDown;

    public bool IsAlreadyBuffed { get; private set; }

    private float curBuffCoolDown;

    private void OnEnable()
    {
        curBuffCoolDown = buffCoolDown;
        IsAlreadyBuffed = true;
    }

    private void Update()
    {
        if (IsAlreadyBuffed)
        {
            curBuffCoolDown -= Time.deltaTime;
            if (curBuffCoolDown <= 0)
            {
                curBuffCoolDown = buffCoolDown;
                IsAlreadyBuffed =false;
            }
        }
    }

    public void IncreaseAttackPower(Enemy enemy, float amount)
    {
        enemy.Stat.AttackDamage += amount;
    }

    public void AlreadyBuffed()
    {
        IsAlreadyBuffed = true;
    }
}
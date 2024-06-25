using System.Collections;
using UnityEngine;

public class BuffEffect : PoolObject
{
    private Enemy target;

    private void Update()
    {
        if(target != null)
        {
            // 처리 안해주면 넉백 시 버프가 허공에 뜸
            transform.position = target.transform.position + Vector3.up;
        }
        if(target.Health.IsDie)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetPosition(Enemy enemy)
    {
        target = enemy;
    }
}

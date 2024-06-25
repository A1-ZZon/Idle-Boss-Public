using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomEvent;

public class EnemyDataManager : MonoBehaviour
{
    [Header("# Enemy Texture ")]
    [SerializeField] private Texture[] enemyTextures;
    private List<SkinnedMeshRenderer> spawnEnemyTextureList = new List<SkinnedMeshRenderer>();
    
    [Header("# Enemy Spawn Percent ")]
    [SerializeField] private StageSpawnPercent[] stageSpawnPercent;
    [SerializeField] private int spawnPercentLevel = 0;
    
    private RandomEventResult randomEvent;

    private void Awake()
    {
        randomEvent = new RandomEventResult();

        for (int i = 0; i < stageSpawnPercent.Length; i++)
        {
            stageSpawnPercent[i].CalculatePercentage();
        }
        
    }

    public void SetSpawnPercentLevel(int spawnLevel)
    {
        // 스폰 레벨이 StageSpawnPercent의 길이를 넘으면 안되기 때문에 둘 중 작은 값을 저장
        spawnPercentLevel = Mathf.Min(spawnLevel, stageSpawnPercent.Length - 1);
    }
    
    public Enemy GetEnemy()
    {
        int enemyType = randomEvent.GetArrRandomResult(stageSpawnPercent[spawnPercentLevel].spawnPercent);

        // 0. Small, 1. Medium, 2. Large
        int rand = enemyType switch
        {
            0 => Random.Range((int)EPoolObjectType._StartSmallEnemyType + 1, (int)EPoolObjectType._EndSmallEnemyType),
            1 => Random.Range((int)EPoolObjectType._StartMediumEnemyType + 1, (int)EPoolObjectType._EndMediumEnemyType),
            2 =>Random.Range((int)EPoolObjectType._StartLargeEnemyType + 1, (int)EPoolObjectType._EndLargeEnemyType),
        };
        
        // Enemy 저장
        Enemy enemy = GameManager.Instance.Pool.SpawnFromPool((EPoolObjectType)rand).ReturnMyComponent<Enemy>();

        // 텍스쳐 랜덤 적용
        int t_rand = Random.Range(0, enemyTextures.Length);

        SkinnedMeshRenderer smr = GetSkinnedMeshRenderer(enemy);
        smr.material.mainTexture = enemyTextures[t_rand];
        
        return enemy;
        
    }
    
    private SkinnedMeshRenderer GetSkinnedMeshRenderer(Enemy enemy) // GetComponentInChildren을 최소화하기 위해 SMR 정보를 저장함
    {
        // 기존에 캐싱된 SkinnedMeshRenderer가 있는지 확인
        SkinnedMeshRenderer smr = spawnEnemyTextureList.Find(s => s.gameObject == enemy.gameObject);

        if (smr == null)
        {
            // 없으면 GetComponentInChildren을 사용하여 찾고 리스트에 추가
            smr = enemy.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr != null)
            {
                spawnEnemyTextureList.Add(smr);
            }
            else
            {
                Debug.LogWarning("SkinnedMeshRenderer not found on enemy!");
            }
        }

        return smr;
    }
}

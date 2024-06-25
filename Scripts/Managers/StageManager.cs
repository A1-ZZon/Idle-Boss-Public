using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
   [field: Header("# Stage Info")]
   [field: SerializeField] public int StageNum { get; set; }
   [field: SerializeField] public CubeRotationEventHandler CubeRotationEvent { get; set; }
   public bool isStageReady { get; private set; }
   
   [field:Header("# Enemy Spawn Info")]
   [field:SerializeField] public EnemyStatLevelUpSO EnemyStatLevelUpData { get; private set; } // 적 강화 데이터
   [field:SerializeField] public EnemyDataManager EnemyDataManager { get; private set; } 
   [field:SerializeField]public Transform[] EnemySpawnPoints { get; private set; }
   [field:SerializeField] public int CurrentEnemySpawnNum{ get; private set; }
   [field:SerializeField] public int MaxEnemySpawnNum{ get; private set; } // 최대 적 갯수
   [field:SerializeField] public int EnemyEnforceLevel { get; private set; } // 적 강화레벨
   [field:SerializeField] public List<Enemy> SpawnEnemyList { get; private set; }
   private HashSet<Transform> spawnPointCompleteHashSet = new HashSet<Transform>(); // 중복 포지션 판별 해시셋
   
   [field:Header("# Enemy Spawn Effect Info")]
   [SerializeField] private float enemySpawnDelayTime;
   [SerializeField] private float spawnEffectLerpTime;
   
   private Coroutine enemySpawnCouroutine;
   private WaitForSeconds wait;
   private int StageClearGold;
   public event Action OnStageClear;

   private void Awake()
   {
      SpawnEnemyList = new List<Enemy>();
      GameManager.Instance.Stage = this;
      wait = new WaitForSeconds(enemySpawnDelayTime);
   }

   private IEnumerator Start()
   {
      yield return null;
      StartNewStage();
   }

   public void StageClear()
   {
      isStageReady = false;
      StageNum++;
        GameManager.Instance.Player.SaveData.SetStageLevel(StageNum);
      SetEnemySpawnInfo(); 
      CubeRotationEvent.RotateCube(ERotateType.RotateX);
      GameManager.Instance.Player.CallChangeGold(StageClearGold);
        GameManager.Instance.PlayerData.DataSave(GameManager.Instance.Player.SaveData);
      CallStageClear();
    }

    private void CallStageClear()
    {
        OnStageClear?.Invoke();
    }

    private void CalculateReward()
    {
        foreach(var enemy in SpawnEnemyList)
        {
            Debug.Log(enemy.Stat.Gold);
        }
        Debug.Log("----------------------------");
        StageClearGold = SpawnEnemyList.Sum(enemy => enemy.Stat.Gold);
    }
    private void SetEnemySpawnInfo()
   {
      // 소환 마릿수, 강화레벨 적용 
      CurrentEnemySpawnNum = StageNum % MaxEnemySpawnNum;
      if (CurrentEnemySpawnNum == 0)
      {
         CurrentEnemySpawnNum = MaxEnemySpawnNum;
      }
      
      EnemyEnforceLevel = (StageNum- 1) / MaxEnemySpawnNum;
   }

    public void SetStageLevel(int level)
    {
        StageNum = level;
        SetEnemySpawnInfo();
    }

   public void StartNewStage() //  스테이지 시작 함수
   {
      // TODO: 스테이지 시작할 떄 효과나 이런 것들 추가할 예정
      SpawnEnemy();
   }

   private void SpawnEnemy()
   {
      SpawnEnemyReset();
      EnemyDataManager.SetSpawnPercentLevel(EnemyEnforceLevel);
      
      if (enemySpawnCouroutine != null)
      {
         StopCoroutine(enemySpawnCouroutine);
      }
      enemySpawnCouroutine = StartCoroutine(SpawnEnemyCouroutine());
 
   }

   private IEnumerator SpawnEnemyCouroutine()
   {
      for (int i = 0; i < CurrentEnemySpawnNum; i++)
      {
         SpawnEffect effect = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.SpawnEffect).ReturnMyComponent<SpawnEffect>();
         effect.OnSpawnEnemy += ActivateEnemy;
         
         while (true) // 위치 설정
         {
            int rand = Random.Range(0, EnemySpawnPoints.Length);

            if (spawnPointCompleteHashSet.Add(EnemySpawnPoints[rand])) // 해시 셋은 중복 추가가 안되기 때문에 중복 포지션이라면 false 반환
            {
               effect.Init(EnemySpawnPoints[rand].position, spawnEffectLerpTime);
               break;
            }
         }
         yield return wait;
      }
   }

   private void ActivateEnemy(Vector3 pos)
   {
      Enemy enemy = EnemyDataManager.GetEnemy();
      SpawnEnemyList.Add(enemy);
      enemy.transform.position = pos;
      
      if (CurrentEnemySpawnNum == SpawnEnemyList.Count)
      {
         StartCoroutine(StartStageCoroutine());
      }
   }

   private IEnumerator StartStageCoroutine()
   {
      yield return wait;
      isStageReady = true;
      CalculateReward();
    }

   private void SpawnEnemyReset() // 소환된 Enemy 리셋
   {
      foreach (Enemy enemy in SpawnEnemyList)
      {
         enemy.gameObject.SetActive(false);
      }
      
      SpawnEnemyList.Clear();
      spawnPointCompleteHashSet.Clear();
   }
   
   
}

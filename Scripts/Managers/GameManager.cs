using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field:SerializeField] public PoolManager Pool { get;  set; }
    [field:SerializeField] public StageManager Stage { get;  set; }
    [field: SerializeField] public PlayerDataManager PlayerData { get; set; }
    [field:SerializeField] public Player Player { get;  set; }
}
 
 
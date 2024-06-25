using UnityEngine;

public class RangedEnemy : Enemy
{
    [field : SerializeField] public Transform bulletSpawnPos {  get; private set; }
}

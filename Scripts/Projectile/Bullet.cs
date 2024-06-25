using System;
using UnityEngine;

public class Bullet : PoolObject
{
    public event Action OnHitBullet;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;

    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 bulletDir;

    private Rigidbody rigidbody;
    private float lifeTimer;
    private bool isShot = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
         Init();
    }

    private void Update()
    {
        if (!isShot)
        {
            Shoot();
            isShot = true;
        }

        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            gameObject.SetActive(false);
            OnHitBullet = null;
        }
    }

    void Init()
    {
        bulletDir = Vector3.zero;
        // bulletLifeTime 초기화
        lifeTimer = bulletLifeTime;
        // 이전 총알 속도 제거
        rigidbody.velocity = Vector3.zero;
        isShot = false;
    }

    void Shoot()
    {
        // 위치 세팅
        startPos = transform.position;
        targetPos = GameManager.Instance.Player.transform.position + Vector3.up * 15;
        bulletDir = GetBulletDirection();

        // 발사
        rigidbody.AddForce(bulletDir * bulletSpeed, ForceMode.Impulse);
    }

    Vector3 GetBulletDirection()
    {
        return (targetPos - startPos).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            gameObject.SetActive(false);
            OnHitBullet?.Invoke();
            OnHitBullet = null;
        }
    }
}

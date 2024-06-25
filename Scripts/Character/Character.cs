using System.Collections;
using UnityEngine;

public class Character : PoolObject
{
    [field: SerializeField] public CharacterSO Data { get; private set; }
    [field: SerializeField] public AnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public Health Health { get; private set; }

    private WaitForSeconds animLength;

    protected virtual void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();     
        Health = GetComponent<Health>();

    }

    protected virtual void OnEnable()
    {
        Health.OnDie += Die;
    }

    protected virtual void OnDisable()
    {
        Health.OnDie -= Die;
    }

    public void Die()
    {
        StartCoroutine(WaitForDieAnim());
    }

    IEnumerator WaitForDieAnim()
    {
        Animator.SetTrigger(AnimationData.DieParameterHash);

        if(animLength == null)
        {
            animLength = new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);
        }

        yield return animLength;

        gameObject.SetActive(false);
    }

    public void KnockBack(float forcedTime, Vector3 dir)
    {
        StartCoroutine(KnockBackCoroutine(forcedTime, dir));        
    }
    IEnumerator KnockBackCoroutine(float forcedTime, Vector3 dir)
    {
        Vector3 dampingVelocity = Vector3.zero;
        //Controller.enabled = false;
        while (forcedTime > 0)
        {
            Vector3 force = Vector3.SmoothDamp(transform.position, dir, ref dampingVelocity, 0.8f);
            //transform.position = force;
            force = force - transform.position;
            Controller.Move(force);
            forcedTime -= Time.deltaTime;
            yield return null;
        }
        //Controller.enabled = true;
    }
    
}
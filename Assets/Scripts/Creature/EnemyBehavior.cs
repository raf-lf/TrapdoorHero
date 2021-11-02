using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    public bool searching = true;
    public float attackCooldown;
    public float readyAttackTime;
    protected float busyTimer;
    protected Animator animator;
    protected Rigidbody rb;
    public EnemyPointer pointer;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        rb = transform.parent.gameObject.GetComponentInChildren<Rigidbody>();
        pointer = transform.parent.gameObject.GetComponentInChildren<EnemyPointer>();

    }
    protected virtual void Start()
    {
        busyTimer = Random.Range(attackCooldown * 1, attackCooldown * 2);
        pointer.anim.SetBool("disabled", false);

    }

    public void Death()
    {
        pointer.anim.SetBool("disabled", true);
        StopAllCoroutines();
        searching = false;
    }
    public virtual void AttackEffect()
    {   
    }

    public IEnumerator ReadyAttack()
    {
        pointer.anim.SetBool("danger", true);
        animator.SetTrigger("ready");
        yield return new WaitForSeconds(readyAttackTime);
        StartCoroutine(Attack());

    }
    public IEnumerator Attack()
    {
        pointer.anim.SetBool("danger", false);
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackCooldown);
        searching = true;

    }    

    public virtual void Searching()
    {

    }
    protected virtual void Update()
    {
        if (busyTimer > 0)
            busyTimer = Mathf.Clamp(busyTimer -= Time.deltaTime, 0, Mathf.Infinity);
        else
        {
            if (searching)
                Searching();
        }
    }
}

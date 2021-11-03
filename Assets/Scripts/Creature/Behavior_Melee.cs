using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Melee : EnemyBehavior
{
    public float attackRange;
    public ParticleSystem vfxAttack;
    public float moveSpeed;

    public override void AttackEffect()
    {
        base.AttackEffect();

        if (vfxAttack != null)
            vfxAttack.Play();

    }
    public void SetMove(bool on)
    {
        animator.SetBool("move", on);

    }
    
    public void Moving()
    {
        transform.LookAt(GameManager.scriptPlayer.transform.position);

        if (moveSpeed != 0)
            rb.position += rb.transform.forward * moveSpeed * Time.deltaTime;

    }

    private bool PlayerNearby()
    {
        if (Vector3.Distance(transform.position, GameManager.scriptPlayer.gameObject.transform.position) <= attackRange)
        { 
           // Debug.Log(Vector3.Distance(transform.position, GameManager.scriptPlayer.gameObject.transform.position));
            return true;
        }
        else return false;

    }

    public override void Searching()
    {
        base.Searching();

        if (PlayerNearby())
        {
            SetMove(false);
            searching = false;
            StartCoroutine(ReadyAttack());
        }
        else SetMove(true);
    }

    protected override void Update()
    {
        base.Update();
        Moving();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

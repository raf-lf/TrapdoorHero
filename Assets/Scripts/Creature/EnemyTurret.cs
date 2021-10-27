using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileOrigin;
    public float attackCooldown;
    private float attackCooldownTimer;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        attackCooldownTimer = Random.Range(attackCooldown * .75f, attackCooldown * .125f);
    }

    public void AttackEffect()
    {
        GameObject shot = Instantiate(projectile);
        shot.transform.position = projectileOrigin.position;
        shot.transform.LookAt(GameManager.scriptPlayer.GetComponent<Rigidbody>().transform);

        shot.GetComponent<Projectile>().Setup();

    }

    public void Attack()
    {
        if (attackCooldownTimer > 0)
            attackCooldownTimer = Mathf.Clamp(attackCooldownTimer - Time.deltaTime, 0, Mathf.Infinity);
        else
        {
            animator.SetTrigger("attack");
            attackCooldownTimer = attackCooldown;
        }
    }

    private void Update()
    { 
        Attack();
    }
}

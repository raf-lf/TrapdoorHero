using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileOrigin;
    public float attackCooldown;
    private float attackCooldownTimer;

    public void AttackEffect()
    {
        GameObject shot = Instantiate(projectile);
        shot.transform.position = projectileOrigin.position;
        shot.transform.LookAt(GameManager.scriptPlayer.GetComponent<Rigidbody>().transform);

        shot.GetComponent<Projectile>().Setup();

    }

    public void Attack()
    {
        if (attackCooldownTimer == 0)
        {
            GetComponentInParent<Animator>().SetTrigger("attack");
        }
    }

    private void Update()
    {
        Attack();
        if (attackCooldownTimer > 0)
            attackCooldownTimer = Mathf.Clamp(attackCooldownTimer - Time.time, 0, Mathf.Infinity);
    }
}

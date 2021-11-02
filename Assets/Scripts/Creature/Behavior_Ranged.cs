using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Ranged : EnemyBehavior
{
    public GameObject projectile;
    public Transform projectileOrigin;
    public ObjectPool pool;

    protected override void Start()
    {
        base.Start();
        pool = GameManager.scriptPool.RequestPool(projectile, 5);
        
    }
    public override void AttackEffect()
    {
        base.AttackEffect();

        GameObject shot = pool.GetFromPool();
        shot.transform.position = projectileOrigin.position;
        shot.transform.LookAt(GameManager.scriptPlayer.GetComponent<Rigidbody>().transform);

        shot.GetComponent<Projectile>().Setup();

    }

    public override void Searching()
    {
        base.Searching();
        searching = false;
        StartCoroutine(ReadyAttack());
    }

}

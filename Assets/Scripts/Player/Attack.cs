using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileOrigin;
    public ObjectPool pool;

    private void Start()
    {
        pool = GameManager.scriptPool.RequestPool(projectile, 10);
    }
    public void ShootAttack()
    {
        GameObject shot = pool.GetFromPool();
        shot.transform.position = projectileOrigin.position;
        shot.transform.LookAt(GameManager.scriptPlayerCamera.aimTarget);

        shot.GetComponent<Projectile>().Setup();

    }
}

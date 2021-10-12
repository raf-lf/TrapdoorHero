using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileOrigin;

    public void ShootAttack()
    {
        GameObject shot = Instantiate(projectile);
        shot.transform.position = projectileOrigin.position;
        shot.transform.LookAt(GameManager.scriptPlayerCamera.aimTarget);

        shot.GetComponent<Projectile>().Setup();

    }
}

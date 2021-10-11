using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : CauseDamage
{
    public int projectileSpeed;

    public void Setup()
    {
        GetComponent<Rigidbody>().velocity = projectileSpeed * transform.forward;

    }

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        particle.transform.parent = null;
        Destroy(gameObject);

        if (collision.gameObject.GetComponent<Health>())
        {
            Damage(collision.gameObject.GetComponent<Health>());
        }
    }
}

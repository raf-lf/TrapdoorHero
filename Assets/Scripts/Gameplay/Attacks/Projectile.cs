using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : CauseDamage
{
    public int projectileSpeed;
    public float selfDestroyTime;
    private ParticleSystem particle;

    private void Start()
    {
        Destroy(gameObject, selfDestroyTime);
        particle = GetComponentInChildren<ParticleSystem>();
    }

    public void Setup()
    {
        GetComponent<Rigidbody>().velocity = projectileSpeed * transform.forward;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (particle != null)
        {
            particle.Play();
            particle.transform.parent = null;
        }
        Destroy(gameObject);

        if (collision.gameObject.GetComponent<Health>())
        {
            Damage(collision.gameObject.GetComponent<Health>());
        }
    }
}

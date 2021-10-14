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
    
    public void Impact()
    {
        if (particle != null)
        {
            particle.Play();
            particle.transform.parent = null;
        }
        if (GetComponentInChildren<PlaySfx>())
            GetComponentInChildren<PlaySfx>().PlayInspectorSfx();

        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            Damage(collision.gameObject.GetComponent<Health>());
        }
        Impact();
    }
}

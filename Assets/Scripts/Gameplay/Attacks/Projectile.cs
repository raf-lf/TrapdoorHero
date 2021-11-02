using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : CauseDamage
{
    public int projectileSpeed;
    public float selfDestroyTime;
    private Animator anim;
    public ParticleSystem vfxTravel;
    public ParticleSystem vfxImpact;
    public PlaySfx sfxBirth;
    public PlaySfx sfxImpact;
    public AudioSource loopSource;

    private void Start()
    {
        StartCoroutine(SelfDestruct());

    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestroyTime);
        StartCoroutine(Destroy(0));

    }
    public IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponentInParent<ObjectPool>().ReturnToPool(gameObject);

    }

    public void Setup()
    {
        if(sfxBirth != null)
            sfxBirth.PlayInspectorSfx();

        EnableDisableProjectile(true);
        GetComponent<Rigidbody>().velocity = projectileSpeed * transform.forward;

    }

    public void Impact()
    {
        if (sfxImpact != null)
            sfxImpact.PlayInspectorSfx();
        if (vfxImpact != null)
            vfxImpact.Play();
        EnableDisableProjectile(false);
        StopCoroutine(SelfDestruct());
        StartCoroutine(Destroy(vfxImpact.main.duration));


    }

    private void EnableDisableProjectile(bool on)
    {
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            item.enabled = on;
        }
        GetComponent<Collider>().enabled = on;
        GetComponent<Rigidbody>().isKinematic = !on;

        if (GetComponentInChildren<TrailRenderer>())
            GetComponentInChildren<TrailRenderer>().Clear();

        if (vfxTravel != null)
        {
            if (on)
                vfxTravel.Play();
            else
                vfxTravel.Stop();
        }

        if(loopSource != null)
        loopSource.enabled = on;
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
        Impact();
        if (collision.gameObject.GetComponent<Health>())
        {
            Damage(collision.gameObject.GetComponent<Health>());
        }
    }
}

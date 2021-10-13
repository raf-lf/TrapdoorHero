using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public bool blocking;
    public bool dodging;

    public ParticleSystem vfxMagicWeapon;
    public ParticleSystem vfxMagicShield;

    [HideInInspector]
    public Animator anim;

    public Transform aimTarget;

    private void Awake()
    {
        GameManager.scriptPlayerCamera = this;
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        GameManager.scriptPlayer.blocking = blocking;
        GameManager.scriptPlayer.dodging = dodging;
    }

    public void BuffVfx(ParticleSystem buff, bool apply)
    {
        ParticleSystem[] particles = buff.GetComponentsInChildren<ParticleSystem>();

        foreach(ParticleSystem particle in particles)
        {
            var emission = particle.emission;
            emission.enabled = apply;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public float hp;
    [HideInInspector]
    public float hpMax;

    public List<AttackType> attackableBy = new List<AttackType>();

    public bool dead;

    public ParticleSystem vfxDamage;
    public ParticleSystem vfxHeal;

    private void Awake()
    {
        hpMax = hp;
    }

    public void Damage(int value, AttackType attackType)
    {
        if(attackableBy.Contains(attackType))
            HPChange(-value);
    }

    public void HPChange(int value)
    {
        if (value < 0 && vfxDamage != null)
            vfxDamage.Play();
        if (value > 0 && vfxHeal != null)
            vfxHeal.Play();

        hp = Mathf.Clamp(hp + value, 0, hpMax);

        if(hp == 0)
            Death();
    }

    public void Death()
    {
        dead = true;
        gameObject.SetActive(false);
    }
}

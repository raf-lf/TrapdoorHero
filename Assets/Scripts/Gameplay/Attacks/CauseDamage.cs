using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Player, Basic, Piercing, Dangerous }

public abstract class CauseDamage : MonoBehaviour
{
    public int damage;
    public AttackType type;

    public void Damage(Health target)
    {
        target.Damage(damage, type);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum powerupType { heal, sword, shield}
    public powerupType type;
    public ParticleSystem buffUseVfx;

    private void OnCollisionEnter(Collision collision)
    {
        CauseDamage activatingDamage = collision.gameObject.GetComponentInChildren<CauseDamage>();

        if (activatingDamage != null)
        {
            if(activatingDamage.type == AttackType.Player)
            {
                Activate();
            }

        }
    }

    public void Activate()
    {
        switch (type)
        {
            case powerupType.heal:
                GameManager.scriptPlayer.BuffHeal();
                break;
            case powerupType.sword:
                GameManager.scriptPlayer.BuffSword(true);
                break;
            case powerupType.shield:
                GameManager.scriptPlayer.BuffShield(true);
                break;
        }

        buffUseVfx.transform.parent = null;
        buffUseVfx.Play();
        buffUseVfx.GetComponentInChildren<PlaySfx>().PlayInspectorSfx();

        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : CauseDamage
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            Damage(other.gameObject.GetComponent<Health>());

            if (GetComponentInChildren<PlaySfx>())
                GetComponentInChildren<PlaySfx>().PlayInspectorSfx();
        }

    }
}

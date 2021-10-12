using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSender : MonoBehaviour
{
    public void Attack()
    {
        GetComponentInChildren<EnemyTurret>().AttackEffect();
    }
}

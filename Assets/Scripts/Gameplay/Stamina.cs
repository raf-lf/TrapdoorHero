using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stamina : MonoBehaviour
{
    public float sp;
    [HideInInspector]
    public float spMax;
    public float spRegen;
    public float spRegenCooldown = 1;
    private float spRegenCooldownTargetTime;

    private void Awake()
    {
        spMax = sp;
    }

    public void SpChange(int value)
    {
        GameManager.scriptHud.ChangeSP(value);

        if (value < 0)
        {
            spRegenCooldownTargetTime = Time.time + spRegenCooldown;

        }
        if (value > 0)
        {

        }

        sp = Mathf.Clamp(sp + value, 0, spMax);

    }

    public void Regen()
    {
        if(Time.time >= spRegenCooldownTargetTime)
            sp = Mathf.Clamp(sp + spRegen * Time.deltaTime, 0, spMax);
    }

    private void Update()
    {
        Regen();
    }


}

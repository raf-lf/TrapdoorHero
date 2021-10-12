using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stamina : MonoBehaviour
{
    public float sp;
    [HideInInspector]
    public float spMax;
    public float spRegen;

    private void Awake()
    {
        spMax = sp;
    }

    public void SpChange(int value)
    {
        GameManager.scriptHud.ChangeSP(value);

        if (value < 0)
        {

        }
        if (value > 0)
        {

        }

        sp = Mathf.Clamp(sp + value, 0, spMax);

    }

    public void Regen()
    {
        sp += (spRegen * Time.deltaTime);
    }

    private void Update()
    {
        Regen();
    }


}

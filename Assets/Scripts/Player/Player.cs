using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack")]
    public int attackSpCost;

    [Header("Block")]
    public bool blocking;
    public float staminaBlockRatio = 1;
    private float currentStaminaBlockReduction = 0;
    public float normalReduction = 1;
    public float piercingReduction = .5f;
    public float heavyReduction = 0f;
    private float currentExtraReduction = 0;

    [Header("Dodge")]
    public int dodgeStaminaCost;
    [HideInInspector]
    public bool dodging;

    [Header("Sword Buff")]
    public float attackSpeedMultiplier;

    [Header("Shield Buff")]
    public float buffStaminaBlockReduction = 1;
    public float buffExtraReduction = 1;


    private Health playerHealth;
    private Stamina playerStamina;
    private Collider playerCollider;

    private void Awake()
    {
        GameManager.scriptPlayer = this;
        playerHealth = GetComponent<Health>();
        playerStamina = GetComponent<Stamina>();
        playerCollider = GetComponent<Collider>();
    }

    public void Block()
        => GameManager.scriptPlayerCamera.anim.SetTrigger("block");

    public void Stagger()
    {
        GameManager.scriptPlayerCamera.anim.SetTrigger("stagger");
        Camera.main.GetComponent<Animator>().SetTrigger("stagger");
    }

    public int DamagePlayer(int value, AttackType type)
    {
        int returnedValue = value;

        if (blocking)
        {
            playerStamina.SpChange((int)((float)-value * Mathf.Clamp((staminaBlockRatio - currentStaminaBlockReduction),0, Mathf.Infinity)));

            if (playerStamina.sp <= 0)
                Stagger();
            else
                Block();

            switch (type)
            {
                case AttackType.Basic:
                    return (int)((float)returnedValue * Mathf.Clamp((1 - normalReduction + currentExtraReduction),0, Mathf.Infinity));
                case AttackType.Piercing:
                    return (int)((float)returnedValue * Mathf.Clamp((1 - piercingReduction + currentExtraReduction), 0, Mathf.Infinity));
                case AttackType.Heavy:
                    return (int)((float)returnedValue * Mathf.Clamp((1 - heavyReduction + currentExtraReduction), 0, Mathf.Infinity));
            }


        }

        GameManager.scriptHud.ChangeHP(returnedValue);

        return returnedValue;
    }

    public void BuffAttack(bool apply)
    {
        GameManager.scriptPlayerCamera.anim.SetBool("buffAttack", apply);
        GameManager.scriptPlayerCamera.BuffVfx(GameManager.scriptPlayerCamera.vfxMagicWeapon, apply);
    }

    public void BuffShield(bool apply)
    {
        if (apply)
        {
            currentStaminaBlockReduction += buffStaminaBlockReduction;
            currentExtraReduction += buffExtraReduction;
        }
        else
        {
            currentStaminaBlockReduction -= buffStaminaBlockReduction;
            currentExtraReduction -= buffExtraReduction;
        }

        GameManager.scriptPlayerCamera.BuffVfx(GameManager.scriptPlayerCamera.vfxMagicShield, apply);

    }

    public void Dodge()
    {
        if (playerStamina.sp >= dodgeStaminaCost)
        {
            playerStamina.SpChange(-dodgeStaminaCost);
            GameManager.scriptPlayerCamera.anim.SetTrigger("dodge");
            Camera.main.GetComponent<Animator>().SetTrigger("dodge");
        }
        else
            GameManager.scriptHud.NoSP();
    }

    private void Update()
    {
        if (dodging)
            playerCollider.enabled = false;
        else
            playerCollider.enabled = true;
    }


}

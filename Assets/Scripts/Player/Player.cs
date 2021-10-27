using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool playerControl = false;

    [Header("Attack")]
    public int attackSpCost;

    [Header("Block")]
    public bool blocking;
    public float staminaCostPerDamageBlock = 1;
    private float staminaCostPerDamageBlockExtra = 0;

    public float normalReduction = 1;
    public float piercingReduction = .5f;
    public float heavyReduction = 0f;
    private float modifierReduction = 0;

    [Header("Dodge")]
    public int dodgeStaminaCost;
    [HideInInspector]
    public bool dodging;

    [Header("Heal Buff")]
    public float hpRestored;

    [Header("Sword Buff")]
    public float buffSwordDuration = 30;
    [HideInInspector]
    public float buffSwordDurationCurrent;

    [Header("Shield Buff")]
    public float buffShieldDuration = 30;
    [HideInInspector]
    public float buffShieldDurationCurrent;
    public float buffStaminaCostPerDamageBlock = 1;
    public float buffDamageReduction = 1;


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
            playerStamina.SpChange((int)(-value / Mathf.Clamp((staminaCostPerDamageBlock + staminaCostPerDamageBlockExtra),0, Mathf.Infinity)));

            if (playerStamina.sp <= 0)
                Stagger();
            else
                Block();

            switch (type)
            {
                case AttackType.Basic:
                    return (int)(returnedValue * Mathf.Clamp(1 - (normalReduction + modifierReduction),   0, Mathf.Infinity));
                case AttackType.Piercing:
                    return (int)(returnedValue * Mathf.Clamp(1 - (piercingReduction + modifierReduction), 0, Mathf.Infinity));
                case AttackType.Heavy:
                    return (int)(returnedValue * Mathf.Clamp(1 - (heavyReduction + modifierReduction),    0, Mathf.Infinity));
            }

        }

        GameManager.scriptHud.ChangeHP(returnedValue);

        return returnedValue;
    }

    public void BuffHeal()
    {
        playerHealth.HPChange((int)(playerHealth.hpMax * hpRestored));
    }

    public void BuffSword(bool apply)
    {
        GameManager.scriptHud.buffSwordFill.transform.parent.gameObject.SetActive(apply);

        buffSwordDurationCurrent = buffSwordDuration;
        GameManager.scriptPlayerCamera.anim.SetBool("buffAttack", apply);
        GameManager.scriptPlayerCamera.BuffVfx(GameManager.scriptPlayerCamera.vfxMagicWeapon, apply);
    }

    public void BuffShield(bool apply)
    {
        GameManager.scriptHud.buffShieldFill.transform.parent.gameObject.SetActive(apply);

        if (apply)
        {
            buffShieldDurationCurrent = buffShieldDuration;
            staminaCostPerDamageBlockExtra += buffStaminaCostPerDamageBlock;
            modifierReduction += buffDamageReduction;
        }
        else
        {
            staminaCostPerDamageBlockExtra -= buffStaminaCostPerDamageBlock;
            modifierReduction -= buffDamageReduction;
        }

        GameManager.scriptPlayerCamera.BuffVfx(GameManager.scriptPlayerCamera.vfxMagicShield, apply);

    }

    public void Dodge()
    {
        if (playerStamina.sp >= dodgeStaminaCost)
        {
            playerStamina.SpChange(-dodgeStaminaCost);
            GameManager.scriptPlayerCamera.anim.SetTrigger("dodge");
            GameManager.scriptGameplay.mainCamera.GetComponentInChildren<Animator>().SetTrigger("dodge");
        }
        else
            GameManager.scriptHud.NoSP();
    }

    private void UpdateDodgeCollider()
    {
        if (dodging)
            playerCollider.enabled = false;
        else
            playerCollider.enabled = true;

    }

    public void PlayerDeath()
    {
        GameManager.scriptPlayerCamera.anim.SetTrigger("death");
    }
    private void DecayBuffs()
    {
        if (buffSwordDurationCurrent > 0)
        {
            buffSwordDurationCurrent = Mathf.Clamp(buffSwordDurationCurrent -= Time.deltaTime, 0, buffSwordDuration);
            if (buffSwordDurationCurrent == 0)
                BuffSword(false);
        }

        if (buffShieldDurationCurrent > 0)
        {
            buffShieldDurationCurrent = Mathf.Clamp(buffShieldDurationCurrent -= Time.deltaTime, 0, buffShieldDuration);
            if (buffShieldDurationCurrent == 0)
                BuffShield(false);
        }

    }

    private void Update()
    {
        UpdateDodgeCollider();
        DecayBuffs();
    }

   

}

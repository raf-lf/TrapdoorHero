using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack")]
    public float actionSpeed;

    [Header("Block")]
    public bool blocking;
    public float normalReduction = 1;
    public float piercingReduction = .5f;
    public float heavyReduction = 0f;

    [Header("Dodge")]
    public bool dodging;
    public int dodgeStaminaCost;


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
        => GameManager.scriptPlayerCamera.anim.SetTrigger("stagger");

    public int DamagePlayer(int value, AttackType type)
    {
        int returnedValue = value;

        if (blocking)
        {
            playerStamina.SpChange(-value);

            if (playerStamina.sp <= 0)
                Stagger();
            else
                Block();

            switch (type)
            {
                case AttackType.Basic:
                    return (int)((float)returnedValue * (1 - normalReduction));
                case AttackType.Piercing:
                    return (int)((float)returnedValue * (1 - piercingReduction));
                case AttackType.Heavy:
                    return (int)((float)returnedValue * (1 - heavyReduction));
            }


        }

        GameManager.scriptHud.ChangeHP(returnedValue);

        return returnedValue;
    }

    private void Update()
    {
        if (dodging)
            playerCollider.enabled = false;
        else
            playerCollider.enabled = true;
    }


}

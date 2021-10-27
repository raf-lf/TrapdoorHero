using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    private Health connectedHealth;
    private Stamina connectedStamina;

    [Header("Health")]
    public TextMeshProUGUI hpText;
    public Image hpFill;
    public Image hpFeedbackBar;

    [Header("Stamina")]
    public TextMeshProUGUI spText;
    public Image spFill;
    public Image spFeedbackBar;

    [Header("Buffs")]
    public TextMeshProUGUI buffSwordDuration;
    public Image buffSwordFill;

    public TextMeshProUGUI buffShieldDuration;
    public Image buffShieldFill;

    [Header("Other")]
    public TextMeshProUGUI scoreText;

    [Header("Overlay")]
    public Animator overlayAnimator;
    public Animator hudAnimator;

    private void Awake()
    {
        GameManager.scriptHud = this;
    }

    private void Start()
    {
        connectedHealth = GameManager.scriptPlayer.GetComponent<Health>();
        connectedStamina = GameManager.scriptPlayer.GetComponent<Stamina>();
        UpdateScore();
    }

    public void ChangeHP(float valueChange)
    {
        if (valueChange > 0)
        {
            hpFill.GetComponentInParent<Animator>().SetTrigger("increase");
            overlayAnimator.SetTrigger("heal");
        }

        if (valueChange < 0)
        {
            hpFill.GetComponentInParent<Animator>().SetTrigger("decrease");
            overlayAnimator.SetTrigger("damage");
        }

        if(connectedHealth.hp <=  connectedHealth.hpMax * .33f)
            overlayAnimator.SetBool("lowHp", true);
        else
            overlayAnimator.SetBool("lowHp", false);


    }

    public void ChangeSP(float valueChange)
    {
        if (valueChange > 0)
            spFill.GetComponentInParent<Animator>().SetTrigger("increase");

        if (valueChange < 0)
        {
            spFill.GetComponentInParent<Animator>().SetTrigger("decrease");
            overlayAnimator.SetTrigger("block");
        }
    }

    public void NoHP()
        => hpFill.GetComponentInParent<Animator>().SetTrigger("insufficient");
    public void NoSP()
        => spFill.GetComponentInParent<Animator>().SetTrigger("insufficient");

    private void UpdateValue()
    {
        hpFill.fillAmount = connectedHealth.hp / connectedHealth.hpMax;
        hpFeedbackBar.fillAmount = connectedHealth.hp / connectedHealth.hpMax;
        hpText.text = "HP " + (int)connectedHealth.hp + "/" + (int)connectedHealth.hpMax;

        spFill.fillAmount = connectedStamina.sp / connectedStamina.spMax;
        spFeedbackBar.fillAmount = connectedStamina.sp / connectedStamina.spMax;
        spText.text = "SP " + (int)connectedStamina.sp + "/" + (int)connectedStamina.spMax;
    
        buffSwordFill.fillAmount = GameManager.scriptPlayer.buffSwordDurationCurrent / GameManager.scriptPlayer.buffSwordDuration;
        buffSwordDuration.text = (Mathf.RoundToInt(GameManager.scriptPlayer.buffSwordDurationCurrent).ToString());

        buffShieldFill.fillAmount = GameManager.scriptPlayer.buffShieldDurationCurrent / GameManager.scriptPlayer.buffShieldDuration;
        buffShieldDuration.text = (Mathf.RoundToInt(GameManager.scriptPlayer.buffShieldDurationCurrent).ToString());
        
    }

    public void ChangeScore(int value)
    {
        GameManager.score += value;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = GameManager.score.ToString();
    }

    private void Update()
    {
        UpdateValue();
    }

}

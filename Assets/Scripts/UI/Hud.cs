using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    private Health connectedHealth;
    private Stamina connectedStamina;

    public TextMeshProUGUI hpText;
    public Image hpFill;
    public Image hpFeedbackBar;
    public TextMeshProUGUI spText;
    public Image spFill;
    public Image spFeedbackBar;

    private void Awake()
    {
        GameManager.scriptHud = this;
    }

    private void Start()
    {
        connectedHealth = GameManager.scriptPlayer.GetComponent<Health>();
        connectedStamina = GameManager.scriptPlayer.GetComponent<Stamina>();
    }

    public void ChangeHP(float valueChange)
    {
        if (valueChange > 0)
            hpFill.GetComponentInParent<Animator>().SetTrigger("increase");

        if (valueChange < 0)
            hpFill.GetComponentInParent<Animator>().SetTrigger("decrease");

    }

    public void ChangeSP(float valueChange)
    {
        if (valueChange > 0)
            spFill.GetComponentInParent<Animator>().SetTrigger("increase");

        if (valueChange < 0)
            spFill.GetComponentInParent<Animator>().SetTrigger("decrease");
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

    }

    private void Update()
    {
        UpdateValue();
    }

}

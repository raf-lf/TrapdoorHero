using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureCanvas : MonoBehaviour
{
    private Health connectedHealth;
    public TextMeshProUGUI hpText;
    public Image hpFill;

    private void Start()
    {
        connectedHealth = GetComponentInParent<Health>();
    }
    public void UpdateValues()
    {
        hpText.text = "HP " + connectedHealth.hp + "/" + connectedHealth.hpMax;
        hpFill.fillAmount = ((float)connectedHealth.hp / (float)connectedHealth.hpMax);
        hpFill.transform.parent.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideBar());


    }

    IEnumerator HideBar()
    {
        yield return new WaitForSeconds(2);
        hpFill.transform.parent.gameObject.SetActive(false);
    }
}

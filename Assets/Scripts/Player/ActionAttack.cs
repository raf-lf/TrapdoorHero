using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Attack(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Attack(false);

    }

    public void Attack(bool on)
    {
        if (on && GameManager.scriptPlayer.playerControl)
            GameManager.scriptPlayerCamera.anim.SetBool("attacking", true);
        else
            GameManager.scriptPlayerCamera.anim.SetBool("attacking", false);

    }
    private void Update()
    {
        if (GameManager.scriptGameplay.onPc)
            Attack(Input.GetKey(KeyCode.E));

    }
}

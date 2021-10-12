using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.scriptPlayerCamera.anim.SetBool("attacking", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.scriptPlayerCamera.anim.SetBool("attacking", false);

    }

}

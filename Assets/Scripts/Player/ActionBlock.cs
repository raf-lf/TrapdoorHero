using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.scriptPlayer.anim.SetBool("blocking", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.scriptPlayer.anim.SetBool("blocking", false);
    }

}

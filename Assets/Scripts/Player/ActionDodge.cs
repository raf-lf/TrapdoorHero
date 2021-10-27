using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionDodge : MonoBehaviour, IPointerClickHandler,IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Dodge();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void Dodge()
    {
        if (GameManager.scriptPlayer.playerControl)
            GameManager.scriptPlayer.Dodge();

    }

    private void Update()
    {
        if (GameManager.scriptGameplay.onPc)
            if (Input.GetKeyDown(KeyCode.Space))
                Dodge();


    }
}

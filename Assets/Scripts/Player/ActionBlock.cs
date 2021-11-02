using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Block(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Block(false);
    }

    public void Block(bool on)
    {
        if (on && GameManager.scriptPlayer.playerControl)
            GameManager.scriptPlayerCamera.anim.SetBool("blocking", true);
        else
            GameManager.scriptPlayerCamera.anim.SetBool("blocking", false);

    }

    private void Update()
    {
        if (GameManager.scriptGameplay.onPc)
            Block(Input.GetKey(KeyCode.Q));

    }


}

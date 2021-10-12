using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public bool blocking;
    public bool dodging;

    [HideInInspector]
    public Animator anim;

    public Transform aimTarget;

    private void Awake()
    {
        GameManager.scriptPlayerCamera = this;
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        GameManager.scriptPlayer.blocking = blocking;
        GameManager.scriptPlayer.dodging = dodging;
    }

}

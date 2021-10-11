using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    public Transform aimTarget;

    private void Awake()
    {
        GameManager.scriptPlayer = this;
        anim = GetComponent<Animator>();

    }

}

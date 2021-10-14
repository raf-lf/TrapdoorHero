using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTools: MonoBehaviour
{
    public void PlaySfx(AudioClip clip)
    {
        GameManager.scriptAudio.PlaySfx(clip, 1, Vector2.one);
    }

}

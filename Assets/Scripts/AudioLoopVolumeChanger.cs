using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopVolumeChanger : MonoBehaviour
{
    public AudioSource loopSource;

    private void Awake()
    {
        if (loopSource == null && GetComponent<AudioSource>())
            loopSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        loopSource.volume = GameManager.scriptAudio.ReturnSfxVolume();
    }

}

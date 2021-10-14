using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    public bool playOnStart;
    public AudioClip[] clips = new AudioClip[1];
    public float volume;
    public Vector2 pitchVariance;

    private void Start()
    {
        if (playOnStart)
            PlayInspectorSfx();
    }

    public void PlayInspectorSfx()
    {
        GameManager.scriptAudio.PlaySfx(clips[Random.Range(0,clips.Length)], volume, pitchVariance);

    }

}

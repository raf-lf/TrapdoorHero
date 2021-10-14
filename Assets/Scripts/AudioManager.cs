using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static float volumeSfx = .5f;
    public static float volumeBgm = .25f;
    public AudioSource sfxSource;
    public AudioSource bgmSource;


    public void Awake()
    {
        GameManager.scriptAudio = this;
    }

    public void PlaySfx(AudioClip clip, float volume, Vector2 pitchVariance)
    {
        sfxSource.volume = volume * volumeSfx;
        sfxSource.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        sfxSource.PlayOneShot(clip);

    }

    public void PlayBgm(AudioClip music, float volume)
    {
        bgmSource.volume = volume * volumeBgm;
        bgmSource.clip = music;
        bgmSource.Play();

    }

    /*public void FadeBgm(float volume, float time)
    {

    }

    IEnumerator Fade (float volume, float time)
    {
        for (int i = (int)(time * 60); i > 0 ; i--)
        {
            if()
            yield return new WaitForSeconds(time / 60);

        }
    }
    */
}

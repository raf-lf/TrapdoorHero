using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static float volumeSfxModifier = .5f;
    public static float volumsBgmModifier = .5f;
    public static float volumeCurrentBgm = 1;
    public AudioSource sfxSource;
    public AudioSource bgmSource;


    public void Awake()
    {
        GameManager.scriptAudio = this;
    }

    public void PlaySfx(AudioClip clip, float volume, Vector2 pitchVariance, AudioSource source)
    {
        AudioSource usedSource = sfxSource;

        if (source != null) 
            usedSource = source;

        usedSource.volume = volume * volumeSfxModifier;
        usedSource.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        usedSource.PlayOneShot(clip);

    }

    public void PlayBgm(AudioClip music, float volume)
    {
        volumeCurrentBgm = volume;
        bgmSource.volume = volumeCurrentBgm * volumsBgmModifier;
        bgmSource.clip = music;
        bgmSource.Play();

    }

    public void FadeBgm(float volume, float volumeChange)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(volume, volumeChange));
    }

    public IEnumerator FadeCoroutine (float volume, float volumeChange)
    {
        while (volumeCurrentBgm != volume)
        {
            if (volumeCurrentBgm > volume)
                volumeCurrentBgm = Mathf.Clamp(volumeCurrentBgm - volumeChange, volume, 1);
            else if (volumeCurrentBgm < volume)
                volumeCurrentBgm = Mathf.Clamp(volumeCurrentBgm + volumeChange, 0, volume);

            bgmSource.volume = volumeCurrentBgm;

            yield return new WaitForSeconds(.1F);

            if (volumeCurrentBgm == volume)
                break;

        }

    }

}

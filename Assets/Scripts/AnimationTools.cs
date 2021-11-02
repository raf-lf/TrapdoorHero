using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTools: MonoBehaviour
{
    public void PlaySfx(AudioClip clip)
    {
        AudioSource source = null;

        if(GetComponentInChildren<AudioSource>() != null)
            source = GetComponentInChildren<AudioSource>();
        
        GameManager.scriptAudio.PlaySfx(clip, 1, Vector2.one, source);
    }

    public void PlayerControlOn()
    {
        GameManager.scriptPlayer.playerControl = true;
    }
    public void PlayerControlOff()
    {
        GameManager.scriptPlayer.playerControl = false;
    }

    public void StartGame()
    {
        GameManager.scriptGameplay.StartGame();
    }

    public void NextWave()
    {
        GameManager.scriptGameplay.NextFloor();
    }

    public void ReturnCamera()
    {
        GameManager.scriptGameplay.ReturnCamera();
    }

    public void Darken()
    {
        GameManager.scriptGameplay.ChangeAmbientColor(1);
    }
}

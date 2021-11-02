using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator anim;
    public Animator patchesAnim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        GameManager.scriptGameplay.cineCamera.SetActive(true);
        GameManager.scriptGameplay.mainCamera.SetActive(false);

    }

    public void StartGame()
    {
        GameManager.scriptGameplay.cineCamera.GetComponent<Animator>().SetTrigger("start");
        patchesAnim.SetTrigger("kick");
    }

    public void InvertBool(string animString)
    {
        anim.SetBool(animString, !anim.GetBool(animString));
    }
    public void SetBoolTrue(string animString)
    {
        anim.SetBool(animString, true);
    }
    public void SetBoolFalse(string animString)
    {
        anim.SetBool(animString, false);
    }

    public void LeaveGame()
    {
        Invoke(nameof(Quit), 1.5f);

    }

    public void Quit()
    {
        Application.Quit();
    }
}

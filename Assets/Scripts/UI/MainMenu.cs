using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator anim;
    public Animator patchesAnim;

    private void Start()
    {
        RenderSettings.ambientSkyColor = Color.white;
        anim = GetComponentInParent<Animator>();
        GameManager.scriptGameplay.cineCamera.SetActive(true);
        GameManager.scriptGameplay.mainCamera.SetActive(false);

    }

    public void StartGame()
    {
        anim.SetTrigger("hide");
        GameManager.scriptGameplay.cineCamera.GetComponent<Animator>().SetTrigger("start");
        patchesAnim.SetTrigger("kick");
    }

    public void Credits()
    {
        anim.SetBool("credits", !anim.GetBool("credits"));

    }

    public void LeaveGame()
    {
        Application.Quit();

    }
}

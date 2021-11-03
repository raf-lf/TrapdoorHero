using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private Animator anim;
    public Animator patchesAnim;
    public TextMeshProUGUI recordScore;
    public TextMeshProUGUI recordFloor;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        GameManager.scriptGameplay.cineCamera.SetActive(true);
        GameManager.scriptGameplay.mainCamera.SetActive(false);

        if (PlayerPrefs.HasKey("topFloor"))
            recordFloor.text = PlayerPrefs.GetInt("topFloor").ToString();
        else
            recordFloor.text = 0.ToString();

        if (PlayerPrefs.HasKey("topScore"))
            recordScore.text = PlayerPrefs.GetInt("topScore").ToString();
        else
            recordScore.text = 0.ToString();

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

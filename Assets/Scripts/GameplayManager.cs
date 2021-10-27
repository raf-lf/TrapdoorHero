using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public bool onPc;

    public int wave;
    [Header("Components")]
    public GameObject mainCamera;
    public GameObject cineCamera;
    public Transform roomCenter;
    public Animator trapdoorAnim;

    [Header("Enemies")]
    public GameObject[] enemyList = new GameObject[0];
    public int[] enemyValue = new int[0];

    public int totalValue; 
    [SerializeField]
    private int availableValue;
    public int maxEnemies;
    public int spawnedEnemies;

    [Header("Spawn Points")]
    public Transform spawnPointParent;
    public Transform enemyParent;
    public List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> availableSpawnPoints = new List<Transform>();

    
    private void Awake()
    {
        GameManager.scriptGameplay = this;
        mainCamera.GetComponent<CameraMouse>().enabled = onPc;
        mainCamera.GetComponent<NewGyroCamera>().enabled = !onPc;
    }

    public void StartGame()
    {
        SetupSpawnPoints();
        SpawnEnemies();
        GameManager.scriptHud.hudAnimator.SetBool("hidden", false);

    }

    #region Enemy Spawn
    public void SetupSpawnPoints()
    {
        spawnPoints.Clear();
        spawnPoints.AddRange(spawnPointParent.GetComponentsInChildren<Transform>());

        if (spawnPoints.Contains(spawnPointParent.transform))
            spawnPoints.Remove(spawnPointParent.transform);

        availableSpawnPoints = spawnPoints;
    }

    public void SpawnEnemies()
    {
        float lesserValue = Mathf.Infinity;
        availableValue = totalValue;
        spawnedEnemies = 0;

        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyValue[i] < lesserValue)
                lesserValue = enemyValue[i];

        }
    //    Debug.Log("Lesser value is " + lesserValue);


        for (int i = maxEnemies; i > 0; i--)
        {
            if (availableValue >= lesserValue)
            {
                int roll = Random.Range(0, enemyList.Length);

                if (availableValue >= enemyValue[roll])
                {
                    IndividualSpawn(roll);
                    availableValue -= enemyValue[roll];
                    spawnedEnemies++;

                 //   Debug.Log("Spawned " + spawnedEnemies + "." + "Remaining value is " + availableValue);
                }
            }
            else break;

        }

    }

    public void IndividualSpawn(int index)
    {
        GameObject spawnedEnemy = Instantiate(enemyList[index], enemyParent);

        int roll = Random.Range(0, availableSpawnPoints.Count);

        Transform[] temporaryArray = availableSpawnPoints.ToArray();

        spawnedEnemy.transform.position = temporaryArray[roll].position;
        spawnedEnemy.transform.LookAt(roomCenter.transform);

        availableSpawnPoints.RemoveAt(roll);



    }

    #endregion

    public void EnemyDown()
    {
        spawnedEnemies--;

        if (spawnedEnemies <= 0)
        {
            wave++;
            cineCamera.transform.rotation = mainCamera.transform.rotation;
            cineCamera.SetActive(true);
            mainCamera.SetActive(false);
            cineCamera.GetComponent<Animator>().SetTrigger("fall");
            GameManager.scriptPlayer.playerControl = false;
            trapdoorAnim.SetBool("open", true);
        }
    }

    public void NextWave()
    {
        SetupSpawnPoints();
        SpawnEnemies();
    }
    public void ReturnCamera()
    {
        GameManager.scriptPlayer.playerControl = true;
        mainCamera.transform.rotation = cineCamera.transform.rotation;
        mainCamera.SetActive(true);
        cineCamera.SetActive(false);
    }

    public void PlayerDeath()
    {
        GameManager.scriptHud.hudAnimator.SetBool("hidden", true);
        GameManager.scriptPlayer.playerControl = false;
        cineCamera.SetActive(true);
        mainCamera.SetActive(false);
        cineCamera.GetComponent<Animator>().SetTrigger("death");
        GameManager.scriptHud.overlayAnimator.SetTrigger("death");
        Invoke(nameof(ReloadScene), 5);

    }

    public void ReloadScene()
    {
        GameManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}

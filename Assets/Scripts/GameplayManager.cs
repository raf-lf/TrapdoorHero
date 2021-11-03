using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [HideInInspector]
    public bool onPc;

    [Header("Components")]
    public GameObject mainCamera;
    public GameObject cineCamera;
    public Transform roomCenter;
    public Animator trapdoorAnim;
    public AudioClip music;

    [Header("Difficulty")]
    public int floor = 0;
    public int totalSpawnValue;
    [SerializeField]
    private int availableSpawnValue;
    public int spawnValuePerFloor;
    public int maxEnemies;
    public int floorMultipleEnemyIncrease;
    [SerializeField]
    private int spawnedEnemies;

    [Header("Enemies")]
    public GameObject[] enemyList = new GameObject[0];
    public int[] enemyValue = new int[0];


    [Header("Chests")]
    public GameObject chestPrefab;
    public int chestQty;
    public GameObject healingPowerup;


    [Header("Spawn Points")]
    public Transform spawnPointParent;
    public Transform enemyParent;
    public List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> availableSpawnPoints = new List<Transform>();

    [Header("Environment")]
    public Light globalLight;
    public Color32 outsideAmbientColor;
    public Color32 insideAmbientColor;



    private void Awake()
    {
        GameManager.scriptGameplay = this;

        bool gyroAvailable;

        //gyroAvailable = SystemInfo.supportsGyroscope;
        if (SystemInfo.deviceType == DeviceType.Desktop)
            gyroAvailable = false;
        else 
            gyroAvailable = true;

        Debug.Log("Gyroscope Availability: " + gyroAvailable);

        onPc = !gyroAvailable;
        mainCamera.GetComponent<CameraMouse>().enabled = !gyroAvailable;
        mainCamera.GetComponent<NewGyroCamera>().enabled = gyroAvailable;
    }
    private void Start()
    {
        ChangeAmbientColor(0);
        GameManager.scriptPlayer.playerControl = false;
    }

    public void ChangeAmbientColor(int ambient)
    {
        switch (ambient)
        {
            case 0:
                RenderSettings.ambientSkyColor = outsideAmbientColor;
                //RenderSettings.ambientSkyColor = Color.white;
                globalLight.intensity = 1;
                break;
            case 1:
                RenderSettings.ambientSkyColor = insideAmbientColor;
                //RenderSettings.ambientSkyColor = Color.black;
                globalLight.intensity = 0;
                break;

        }
    }

    public void StartGame()
    {
        floor++;
        GameManager.scriptHud.UpdateFloor();
        SetupSpawnPoints();
        SpawnEnemies();
        SpawnChests();
        GameManager.scriptHud.hudAnimator.SetBool("hidden", false);
        GameManager.scriptAudio.PlayBgm(music, 1);

    }

    public void UpdateDifficulty()
    {
        totalSpawnValue += spawnValuePerFloor;

        if (floor % floorMultipleEnemyIncrease == 0)
            maxEnemies++;
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
        availableSpawnValue = totalSpawnValue;
        spawnedEnemies = 0;

        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyValue[i] < lesserValue)
                lesserValue = enemyValue[i];

        }
    //    Debug.Log("Lesser value is " + lesserValue);


        for (int i = maxEnemies; i > 0; i--)
        {
            if (availableSpawnPoints.Count > 0)
            {
                if (availableSpawnValue >= lesserValue)
                {
                    IndividualSpawn(RollEnemy());
                    spawnedEnemies++;
                }
                else break;
            }
            else break;

        }

    }

    public int RollEnemy()
    {
        int selectedId = 0;
        int roll = Random.Range(0, enemyList.Length);

        if (availableSpawnValue >= enemyValue[roll])
        {
            selectedId = roll;
            availableSpawnValue -= enemyValue[roll];
            return selectedId;
        }
        else
        {
            return RollEnemy();
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

    public void SpawnChests()
    {
        for (int i = chestQty; i > 0; i--)
        {
            if (availableSpawnPoints.Count > 0)
            {
                GameObject chest = Instantiate(chestPrefab, transform);

                int roll = Random.Range(0, availableSpawnPoints.Count);

                Transform[] temporaryArray = availableSpawnPoints.ToArray();

                chest.transform.position = temporaryArray[roll].position;
                chest.transform.LookAt(roomCenter.transform);

                availableSpawnPoints.RemoveAt(roll);
            }
            else break;

            if (availableSpawnPoints.Count > 0 && floor % 5 == 0)
            {
                GameObject heal = Instantiate(healingPowerup, transform);

                int roll = Random.Range(0, availableSpawnPoints.Count);

                Transform[] temporaryArray = availableSpawnPoints.ToArray();

                heal.transform.position = temporaryArray[roll].position;
                heal.transform.LookAt(roomCenter.transform);

                availableSpawnPoints.RemoveAt(roll);
            }
        }

    }

    public void ClearFloor()
    {
        foreach (var item in enemyParent.GetComponentsInChildren<Enemy>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in transform.parent.GetComponentsInChildren<RandomObjectSpawner>())
        {
            Destroy(item.gameObject);

        }
        foreach (var item in transform.parent.GetComponentsInChildren<Powerup>())
        {
            Destroy(item.gameObject);

        }
    }

    public void EnemyDown()
    {
        spawnedEnemies--;

        if (spawnedEnemies <= 0)
        {
            Invoke(nameof(FloorCompleted), 1.5f);
        }
    }

    public void FloorCompleted()
    {
        cineCamera.transform.rotation = mainCamera.transform.rotation;
        cineCamera.SetActive(true);
        mainCamera.SetActive(false);
        cineCamera.GetComponent<Animator>().SetTrigger("fall");
        GameManager.scriptPlayer.playerControl = false;
        trapdoorAnim.SetBool("open", true);

    }
    public void NextFloor()
    {
        floor++;
        GameManager.scriptHud.UpdateFloor();

        UpdateDifficulty();
        ClearFloor();
        SetupSpawnPoints();
        SpawnEnemies();
        SpawnChests();
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
        GameManager.scriptAudio.FadeBgm(0, .05f);
        GameManager.scriptAudio.FadeSfx(0, .05f);

        GameManager.scriptHud.hudAnimator.SetBool("hidden", true);
        GameManager.scriptPlayer.playerControl = false;
        cineCamera.SetActive(true);
        mainCamera.SetActive(false);
        cineCamera.GetComponent<Animator>().SetTrigger("death");
        GameManager.scriptHud.overlayAnimator.SetTrigger("death");
        Invoke(nameof(ReloadScene), 5);

        UpdateRecords();
    }

    public void UpdateRecords()
    {

        if (PlayerPrefs.HasKey("topScore"))
        {
            if (GameManager.score > PlayerPrefs.GetInt("topScore"))
                PlayerPrefs.SetInt("topScore", GameManager.score);
        }
        else
            PlayerPrefs.SetInt("topScore", GameManager.score);

        if (PlayerPrefs.HasKey("topFloor"))
        {
            if (floor > PlayerPrefs.GetInt("topFloor"))
                PlayerPrefs.SetInt("topFloor", floor);
        }
        else
            PlayerPrefs.SetInt("topFloor", floor);
    }
    public void ReloadScene()
    {
        GameManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnApplicationQuit()
    {
        UpdateRecords();
    }
}

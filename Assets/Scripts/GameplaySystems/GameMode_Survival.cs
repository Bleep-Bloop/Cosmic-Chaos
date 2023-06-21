using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode_Survival : MonoBehaviour
{
    public static GameMode_Survival Instance;

    [Header("UI")]
    [SerializeField] private Canvas playerHUD;
    [SerializeField] private TextMeshProUGUI killCounterTextBox;

 
    [SerializeField] private int killCounter;

    [SerializeField] private float increaseDifficultyInterval; // Seconds between calls of IncreaseDifficulty.
    private float enemyHealthMultiplier; // Enemy health is multiplied by this number on their spawn.
    [SerializeField] private float enemyHealthMultiplierIncrease; // Amount enemyHealthMultiplier increases every call of increaseDifficulty().
    [SerializeField] private float bossSpawnInterval; // Seconds between boss enemy spawns

    [Header("Ads")]
    [SerializeField] public bool watchingRewardedAd;
    [SerializeField] public bool hasRevived = false;

    [Header("Death Menu")]
    [SerializeField] private Canvas deathMenuCanvas;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;

    [Header("Components")]
    private FloatingJoystick floatingJoystick;

    private void Awake()
    {
        Instance = this;
        enemyHealthMultiplier = 1.0f;
        enemyHealthMultiplierIncrease = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        watchingRewardedAd = false;
        killCounter = 0;
        UpdateKillCountTextBox();
        InvokeRepeating("IncreaseDifficulty", increaseDifficultyInterval, increaseDifficultyInterval);
        InvokeRepeating("SpawnBoss", bossSpawnInterval, bossSpawnInterval);
        hasRevived = false;
        floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug Show Rewards
        if (Input.GetKey(KeyCode.R))
        {
            UnityAdsManager.Instance.ShowRewardedAd();
            watchingRewardedAd = true;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        floatingJoystick.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        floatingJoystick.gameObject.SetActive(true);
    }

    public void IncrementKillCounter()
    {
        killCounter++;
        UpdateKillCountTextBox();
    }

    private void UpdateKillCountTextBox()
    {
        killCounterTextBox.SetText("Kills: " + killCounter);
    }

    // Invoked every difficultyIncreaseIncrement
    private void IncreaseDifficulty()
    {
        enemyHealthMultiplier += enemyHealthMultiplierIncrease; 
    }

    public float GetEnemyHealthMultipler()
    {
        return enemyHealthMultiplier;
    }

    public void OpenReviveMenu()
    {
        if(!hasRevived)
        {
            deathMenuCanvas.gameObject.SetActive(true);
            acceptButton.onClick.AddListener(StartRewardedAd);
            declineButton.onClick.AddListener(EndGame);
            PauseGame();
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void CloseReviveMenu()
    {
        deathMenuCanvas.gameObject.SetActive(false);
        ResumeGame();
    }

    private void StartRewardedAd()
    {
        UnityAdsManager.Instance.ShowRewardedAd();
        watchingRewardedAd = true;
    }
    public void SkippedRewardedAd()
    {
        EndGame();
    }

    public void CompletedRewardedAd()
    {
        PlayerHealthManager.instance.Revive();
        hasRevived = true;
        CloseReviveMenu();
    }

    /// <summary>
    /// Calls SpawnBossEnemy from games EnemySpawnManager every bossSpawnInterval.
    /// </summary>
    private void SpawnBoss()
    {
        if (EnemySpawnManager.Instance)
            EnemySpawnManager.Instance.SpawnBossEnemy();
    }

}

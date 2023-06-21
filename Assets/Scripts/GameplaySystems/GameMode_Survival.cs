using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Awake()
    {
        Instance = this;
        enemyHealthMultiplier = 1.0f;
        enemyHealthMultiplierIncrease = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        killCounter = 0;
        UpdateKillCountTextBox();
        InvokeRepeating("IncreaseDifficulty", increaseDifficultyInterval, increaseDifficultyInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

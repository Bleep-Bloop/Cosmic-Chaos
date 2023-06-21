using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Button startSurvivalGameButton;
    [SerializeField] private Button startDailyChallengeButton;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        startSurvivalGameButton.onClick.AddListener(StartSurvival);
        startDailyChallengeButton.onClick.AddListener(StartDailyChallenge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSurvival()
    {
        SceneManager.LoadScene("DevScene");
    }

    private void StartDailyChallenge()
    {
        DayOfWeek weekday = DateTime.Today.DayOfWeek;
        switch (weekday)
        {
            case DayOfWeek.Sunday:
                Debug.Log("Load No Movement Mode");
                break;
            case DayOfWeek.Monday:
                Debug.Log("Load Random Upgrades Mode");
                break;
            case DayOfWeek.Tuesday:
                Debug.Log("Load Instant Kill Mode");
                break;
            case DayOfWeek.Wednesday:
                Debug.Log("Load Instant Max Level");
                break;
            case DayOfWeek.Thursday:
                Debug.Log("Load Downgrade Level");
                break;
            case DayOfWeek.Friday:
                Debug.Log("Load Large Upgrade Value Level");
                break;
            case DayOfWeek.Saturday:
                Debug.Log("Load Still Enemies");
                break;
            default:
                break;
        }


    }

}

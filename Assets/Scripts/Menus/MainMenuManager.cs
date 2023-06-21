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
        Debug.Log("Find Day of Week and start level");
    }

}

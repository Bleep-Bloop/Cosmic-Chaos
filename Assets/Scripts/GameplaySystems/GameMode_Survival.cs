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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        killCounter = 0;
        UpdateKillCountTextBox();
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
}

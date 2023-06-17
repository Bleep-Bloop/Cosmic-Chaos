using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{

    public static PlayerHealthManager instance;

    [Header("Components")]
    [SerializeField] private Slider healthSlider;

    [Header("Properties")]
    [SerializeField] private float maxHealth;

    [Header("Runtime")]
    [SerializeField] private float currentHealth;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

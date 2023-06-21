using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{

    public static PlayerHealthManager instance;

    [Header("Components")]
    [SerializeField] private Slider healthBarSlider;

    [Header("Properties")]
    [SerializeField] private float maxHealth;

    [Header("Runtime")]
    [SerializeField] private float currentHealth;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5.0f);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        UpdateHealthBar();

        if(currentHealth <= 0)
        {
            Death();
        }

    }

    public void HealHealth(float healthGained)
    {

        currentHealth += healthGained;

        UpdateHealthBar();

    }

    public void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    public void Death()
    {
        gameObject.SetActive(false);
        if(GameMode_Survival.Instance)
            GameMode_Survival.Instance.OpenReviveMenu();

    }

    public void Revive()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        EnemySpawnManager.Instance.KillAllActiveEnemies();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyBase : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] protected Rigidbody2D rigidBody2D;
    [SerializeField] protected BoxCollider2D boxCollider2D;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Properties")]
    [SerializeField] protected float baseHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float timeBetweenHits = 0.1f; // Time before calling damage again while maintaining contact.
    protected float knockBackTime = 0.5f; // Time enemy is pushed away for (Set through TakeDamage(), passed by DamageEnemyZone).
    [SerializeField] protected int xpValue;

    [Header("Runtime")]
    protected Transform playerCharacter;
    private float hitCountTimer;
    private float knockBackCounter;


    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // ToDo: Get reference from GameManager Singleton when added.
        playerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        
        if(GameMode_Survival.Instance)
        {
            baseHealth *= GameMode_Survival.Instance.GetEnemyHealthMultipler();
        }

        currentHealth = baseHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthManager.instance.gameObject.activeSelf == true)
        {
            if(knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if(movementSpeed > 0)
                {
                    movementSpeed -= movementSpeed * 2.0f;
                }

                if(knockBackCounter <= 0)
                {
                    movementSpeed = Mathf.Abs(movementSpeed * 0.5f);
                }
            }

            rigidBody2D.velocity = (playerCharacter.position - transform.position).normalized * movementSpeed;

            // If enemy hit player begin timeBetweenAttacks countdown
            if (hitCountTimer > 0f)
            {
                hitCountTimer -= Time.deltaTime;
            }
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collision object is on 'Player' layer
        if (collision.gameObject.layer == 3 && hitCountTimer <= 0)
        {
            Debug.Log("[EnemyBase] Hit Player");
            PlayerHealthManager.instance.TakeDamage(damageAmount);

            hitCountTimer = timeBetweenHits;
        }
            
    }

    // Note - If enemy maintains contact this will need to be called.
    private void OnCollisionStay2D(Collision2D collision)
    {
        // If collision object is on 'Player' layer
        if (collision.gameObject.layer == 3 && hitCountTimer <= 0)
        {
            Debug.Log("[EnemyBase] Hit Player (stay)");
            PlayerHealthManager.instance.TakeDamage(damageAmount);

            hitCountTimer = timeBetweenHits;
        }
    }

    /// <summary>
    /// Subtracts Enemy's currentHealth by damageTaken.
    /// Destroys if currentHealth <= 0.
    /// </summary>
    /// <param name="damageTaken">Number to be subtracted from currentHealth.</param>
    /// <param name="knockBack">Enemy will be pushed away on damage.</param>
    public void TakeDamage(float damageTaken, bool knocksBack, float currentKnockBackTime)
    {

        currentHealth -= damageTaken;

        if(knocksBack)
        {
            knockBackTime = currentKnockBackTime;
            knockBackCounter = knockBackTime;
        }

        if(currentHealth <= 0)
        {
            UpgradeManager.instance.SpawnXPOrb(transform.position, xpValue);
            
            if(GameMode_Survival.Instance)
                GameMode_Survival.Instance.IncrementKillCounter();

            Destroy(gameObject);
        }

    }


}

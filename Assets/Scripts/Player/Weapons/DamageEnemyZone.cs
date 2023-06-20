using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageEnemyZone : MonoBehaviour
{
    
    [Header("ENEMY LAYER")]
    [SerializeField] protected LayerMask enemyLayerMask; // Layer that holds enemies.

    [Header("Properties")]
    [SerializeField] protected float damageAmount;
    [SerializeField] protected bool knocksBack; // Enemy will be pushed away on damage.
    [SerializeField] protected float knockBackAmount = 0.1f; // Number of seconds enemy will be pushed away for.
    [SerializeField] protected bool destroyOnDamage; // The object will be destroyed on contact with a single enemy.
    [SerializeField] protected float timeBetweenHits = 0.2f;

    [SerializeField] protected float hitCountTimer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCountTimer > 0f)
        {
            hitCountTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.layer == 6 && hitCountTimer <= 0)
        {
            collision.GetComponent<EnemyBase>().TakeDamage(damageAmount, knocksBack, knockBackAmount);
            
            hitCountTimer = timeBetweenHits;
            
            if (destroyOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If collision object is on 'Enemy' layer
        if (collision.gameObject.layer == 6 && hitCountTimer <= 0)
        {
            Debug.Log("[DamageEnemyZone] Hit Enemy (stay)");
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageAmount, knocksBack, knockBackAmount);

            hitCountTimer = timeBetweenHits;
        }
    }

    public void SetDamageAmount(float newDamageAmount)
    {
        damageAmount = newDamageAmount;
    }

}

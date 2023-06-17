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
    [SerializeField] protected bool destroyOnDamage; // The object will be destroyed on contact with a single enemy.

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.layer == 6)
        {
            collision.GetComponent<EnemyBase>().TakeDamage(damageAmount);

            if(destroyOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }

}

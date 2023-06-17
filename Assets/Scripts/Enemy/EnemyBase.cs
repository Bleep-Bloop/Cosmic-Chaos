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
    [SerializeField] protected float health;
    [SerializeField] protected float movementSpeed;
    
    [Header("Runtime")]
    protected Transform playerCharacter;

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
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody2D.velocity = (playerCharacter.position - transform.position).normalized * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collision object is on 'Player' layer
        if (collision.gameObject.layer == 3)
            Debug.Log("Player Hit");
    }

}

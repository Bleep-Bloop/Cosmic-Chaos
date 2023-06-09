using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class CharacterBase : MonoBehaviour
{

    [Header("Components")]
    protected Rigidbody2D rigidBody2D;
    protected BoxCollider2D boxCollider2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    [Header("Properties")]
    [SerializeField] protected float maxHealth; // ToDo: Seperate into health component.
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float movementSpeed = 2;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        rigidBody2D.isKinematic = true;
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    /// <summary>
    /// Change value of character's movement speed.
    /// </summary>
    /// <param name="newSpeed">Value to be sent to movement speed</param>
    /// <param name="replace">True - newSpeed is the new value of movement speed. False - newSpeed is added to movementSpeed</param>
    public void SetMovementSpeed(float newSpeed, bool replace)
    {
        if (replace)
            movementSpeed = newSpeed;
        else
            movementSpeed += newSpeed;
    }




}

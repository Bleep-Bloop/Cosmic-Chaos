using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserSword : MonoBehaviour
{

    private Animator anim;
    private readonly int SwingAnimationHash = Animator.StringToHash("LaserSwordSwing");
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        SetInActive();
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        spriteRenderer.enabled = true;
        anim.Play(SwingAnimationHash);
    }

    // Called from animation event to deactivate after swing. (ToDo: Sprite Renderer still active.
    public void SetInActive()
    {
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("LaserSwordSwing"))
        {
            // Call function if mid animation to prevent movement direction cancelling swing.
            Invoke("SetInActive", 0.5f);
        }

        spriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }

    public void ApplyUpgradesFromManager(float newDamage, float newRange, Vector3 newSize, float newSpeed)
    {
        Debug.Log("Apply Upgrades From Manager");
        GetComponent<DamageEnemyZone>().SetDamageAmount(newDamage); // Damage 
        transform.localScale = newSize; // Size - General Local Scale
        transform.localScale = new Vector3(newRange, transform.localScale.y, transform.localScale.z); // Range - Increase sword scale only along x axis (range->reach).
        anim.speed = newSpeed; // Speed - Animation Speed
    }

}

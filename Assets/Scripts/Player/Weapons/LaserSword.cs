using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserSword : WeaponBase
{

    private Animator anim;

    private void Awake()
    {
         anim = gameObject.GetComponent<Animator>();
    }

    override protected void Start()
    {
        base.Start();
        SetInActive();
    }

    protected override void Attack()
    {
        gameObject.SetActive(true);
        anim.Play("LaserSwordSwing");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
        }
    }

    public void SetInActive()
    {
        gameObject.SetActive(false);
    }

}

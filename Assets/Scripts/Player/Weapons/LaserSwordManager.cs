using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwordManager : WeaponBase
{

    private CharacterController character;

    [SerializeField] private LaserSword leftSword;
    [SerializeField] private LaserSword rightSword;
    private bool bIsFacingRight;

    protected override void Attack()
    {
        if(bIsFacingRight)
            rightSword.Attack();
        else
            leftSword.Attack();
    }

    private void Awake()
    {
        character = GetComponentInParent<CharacterController>();
    }

    override protected void Start()
    {
        base.Start();
        leftSword.gameObject.SetActive(false);
        rightSword.gameObject.SetActive(false);
    }

    void Update()
    {
        if(character.GetShotDirection().x > 0)
        {
            bIsFacingRight = true;
            rightSword.gameObject.SetActive(true);
            leftSword.SetInActive();
        }
        else
        {
            bIsFacingRight = false;
            leftSword.gameObject.SetActive(true);
            rightSword.SetInActive();
        }
    }

}

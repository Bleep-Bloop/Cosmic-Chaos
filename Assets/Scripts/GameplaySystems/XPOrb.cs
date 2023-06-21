using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class XPOrb : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] private int xpValue;

    [Header("Components")]
    private UpgradeManager playerUpgradeManager;
    

    // Start is called before the first frame update
    void Start()
    {
        playerUpgradeManager = UpgradeManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            playerUpgradeManager.AddXP(xpValue);
            gameObject.SetActive(false); // Retun object to pool
        }
    }

    public void SetXPValue(int newXPValue)
    {
        xpValue = newXPValue;
    }

}

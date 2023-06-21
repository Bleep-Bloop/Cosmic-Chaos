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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            playerUpgradeManager.AddXP(xpValue);
            Destroy(gameObject);
        }
    }

}

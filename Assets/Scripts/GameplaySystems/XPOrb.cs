using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

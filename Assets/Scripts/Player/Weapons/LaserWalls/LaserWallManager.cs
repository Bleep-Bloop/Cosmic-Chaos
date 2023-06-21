using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWallManager : WeaponBase
{

    [Header("Components")]
    [SerializeField] LaserWalls laserWallsPrefabToSpawn;

    [Header("Properties")]
    [SerializeField] float damagePerInterval; // Passed to DamageEnemyZone attached to instantiated LaserWalls.
    [SerializeField] float laserUpTime;

    [Header("Runtime")]
    private bool canActivateLasers;

    protected override void Start()
    {
        base.Start();
        canActivateLasers = true;
    }

    private void Update()
    {
        // Debug Attack() call
        if(Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(laserWallsPrefabToSpawn, transform.position, Quaternion.identity);
        }
    }

    protected override void Attack()
    {
        if (canActivateLasers)
        {
            Instantiate(laserWallsPrefabToSpawn, transform.position, Quaternion.identity);
            canActivateLasers = false;
        }
    }

    public float GetManagerTimeBetweenActivations()
    {
        return timeBetweenActivations;
    }

    public void ResetCanActivateLasers()
    {
        canActivateLasers = true;
    }

    public float GetDamangePerInterval()
    {
        return damagePerInterval;
    }

    protected override void ApplyUpgrade(float newDamage, float newRange, Vector3 newSize, float newSpeed)
    {
        damagePerInterval = newDamage;
        timeBetweenActivations += newRange;
        transform.localScale = newSize;
        laserUpTime += newSpeed;
    }

    public Vector3 GetSize()
    {
        return transform.localScale;
    }

    public float GetLaserUpTime()
    {
        return laserUpTime;
    }
}

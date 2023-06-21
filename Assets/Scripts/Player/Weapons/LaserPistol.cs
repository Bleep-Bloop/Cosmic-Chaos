using UnityEngine;

// Laser Pistol fires in the direction the player is moving.
// Evolved - Laser Rifle fires multiple shots in the direction the player is moving.
public class LaserPistol : WeaponBase
{

    [Header("Components")]
    [SerializeField] private Projectile projectileToSpawn;
    private CharacterController character;
    
    [Header("Properties")]
    [SerializeField] private Transform projectileSpawnLocation;

    // ToDo
    /*
    [Header("Evolved")]
    [SerializeField] private bool isEvolved = false;
    [SerializeField] private float evolvedFireRate = 0.5f;
    */

    protected override void Start()
    {
        base.Start();
        character = GetComponentInParent<CharacterController>();
    }

    void SpawnProjectile()
    {
        Projectile newProjectile = Instantiate(projectileToSpawn, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
        newProjectile.SetShotDirection(character.GetShotDirection());
        ApplyUpgrades(newProjectile);

    }

    protected override void Attack()
    {
        SpawnProjectile();
    }

    protected void ApplyUpgrades(Projectile spawnedProjectile)
    {
        spawnedProjectile.GetComponent<DamageEnemyZone>().SetDamageAmount(damage);
        spawnedProjectile.setTimeAlive(range);
        spawnedProjectile.gameObject.transform.localScale = size;
        spawnedProjectile.SetProjectileSpeed(speed);
    }
}

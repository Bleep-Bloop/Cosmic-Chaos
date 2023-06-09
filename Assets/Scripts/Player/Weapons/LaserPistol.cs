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
        Projectile spawnedProjectile = Instantiate(projectileToSpawn, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
        spawnedProjectile.SetShotDirection(character.GetShotDirection());
    }

    protected override void Attack()
    {
        SpawnProjectile();
    }
}

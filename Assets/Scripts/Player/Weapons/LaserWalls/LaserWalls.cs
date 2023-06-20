using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT - DON'T CHANGE WEAPON PROPERTIES HERE, USE LASER WALL MANAGER.

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DamageEnemyZone))]
public class LaserWalls : WeaponBase
{

    [Header("Components")]
    LaserWallManager laserWallManager;
    [SerializeField] private GameObject dotMarkers; // Game object used to mark position for laser wall ends.
    public List<Transform> dots;
    private LineRenderer lineRenderer;
    private Transform playerCharacter; // Player character transform used to instantiate dotMarkers at location.
    private DamageEnemyZone enemyDamageZone;

    [Header("Properties")]
    [SerializeField] public float laserStartDelay = 0.3f; // Time before collision is started on laser.
    [SerializeField] private float laserUpTime; // Time laser is active before being destroyed.

    [Header("Runtime")]
    [SerializeField] public bool laserStarted;
    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = dots.Count;
        playerCharacter = PlayerHealthManager.instance.transform;
        enemyDamageZone = GetComponent<DamageEnemyZone>();
    }

    override protected void Start()
    {
        base.Start();
        laserWallManager = GameObject.FindGameObjectWithTag("LaserWallManager").GetComponent<LaserWallManager>();
        timeBetweenActivations = laserWallManager.GetManagerTimeBetweenActivations();
        laserStarted = false;
        enemyDamageZone.SetDamageAmount(laserWallManager.GetDamangePerInterval());
    }

    private void Update()
    {
        if(laserStarted)
        {
            lineRenderer.SetPositions(dots.ConvertAll(d => d.position - new Vector3(0, 0, 0)).ToArray());
        }
            
    }

    public float GetWidth()
    {
        return lineRenderer.startWidth;
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    protected override void Attack()
    {
        GameObject newDotMarker = Instantiate(dotMarkers, playerCharacter.transform.position, Quaternion.identity);
        dots.Add(newDotMarker.transform);
        lineRenderer.positionCount = dots.Count;
    }

    public void StartLasers()
    {
        // Check if there has been any changes to damage before activating.
        enemyDamageZone.SetDamageAmount(laserWallManager.GetDamangePerInterval());

        laserStarted = true;

        // Destroy everything after laserUpTime has elapsed.
        Invoke("DestroyLaserWallSystem", laserUpTime);

    }

    // Destroys LaserWall and all created markers.
    public void DestroyLaserWallSystem()
    {
        foreach (Transform markers in dots)
        {
            Destroy(markers.gameObject);
        }
        // Allow LaserWallManager to create new walls
        laserWallManager.ResetCanActivateLasers();
        Destroy(gameObject);
    }
}

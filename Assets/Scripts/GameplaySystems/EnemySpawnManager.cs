using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> spawnableEnemies;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnableBossEnemies;
    private List<GameObject> spawnedBossEnemies = new List<GameObject>();

    [Header("Properties")]
    [SerializeField] private float timeBetweenSpawns; 
    private float spawnTimer;
    [SerializeField] private int checkPerFrame;
    private int enemyToCheck;

    [SerializeField] private Transform minSpawn;
    [SerializeField] private Transform maxSpawn;
    private float despawnDistance;

    private Transform playerCharacter;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnTimer = timeBetweenSpawns;

        playerCharacter = PlayerHealthManager.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4;

    }


    void Update()
    {

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = timeBetweenSpawns;

            int RandomEnemyChoice = Random.Range(0, spawnableEnemies.Count);

            GameObject newEnemy = Instantiate(spawnableEnemies[RandomEnemyChoice], SelectSpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }

        transform.position = playerCharacter.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        while(enemyToCheck < checkTarget)
        {
            if(enemyToCheck < spawnedEnemies.Count)
            {
                if(spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        // Remove cell and move back to be in place
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                        enemyToCheck++;
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }

        // Debug Spawn Boss
        if(Input.GetKeyDown(KeyCode.B))
        {
            SpawnBossEnemy();
        }

    }

    public void SpawnBossEnemy()
    {
        int RandomBossChoice = Random.Range(0, spawnableBossEnemies.Count);
        GameObject newBossEnemy = Instantiate(spawnableBossEnemies[RandomBossChoice], SelectSpawnPoint(), transform.rotation);
        spawnedBossEnemies.Add(newBossEnemy);
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0.1f, 1.0f) > .5f;

        if(spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0.0f, 1.0f) > .5f)
                spawnPoint.x = maxSpawn.position.x;
            else
                spawnPoint.x = minSpawn.position.x;
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0.0f, 1.0f) > 0.5f)
                spawnPoint.y = maxSpawn.position.y;
            else
                spawnPoint.y = minSpawn.position.y;
        }
        return spawnPoint;
    }

    public void KillAllActiveEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(1000, false, 0);
        }
    }

}

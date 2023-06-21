using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 2;
    private Vector3 shotDirection;
    private float timeAlive = 3;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMyself", timeAlive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shotDirection.normalized * projectileSpeed * Time.deltaTime;      
    }

    public void SetProjectileSpeed(float newSpeed)
    {
        projectileSpeed = newSpeed;
    }

    public void SetShotDirection(Vector3 direction)
    {
        shotDirection = direction;
    }

    public void DestroyMyself()
    {
        Destroy(gameObject);
    }

    public float getTimeAlive()
    {
        return timeAlive;
    }

    public void setTimeAlive(float newTimeAlive)
    {
        timeAlive = newTimeAlive;
    }

}

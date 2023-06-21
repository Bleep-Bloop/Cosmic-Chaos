using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] private Camera gameCamera;

    private void Start()
    {
        gameCamera = Camera.main;
    }

    void Update()
    {
       gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, gameCamera.transform.position.z);
    }
}

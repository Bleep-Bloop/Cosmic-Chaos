using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class CharacterController : MonoBehaviour
{

    [Header("Components")]
    private FloatingJoystick floatingJoystick;
    private Rigidbody2D rb2D;
    private CharacterBase character;

    private Vector3 movementVector = new Vector3();

    private void Awake()
    {
        floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
        character = GetComponent<CharacterBase>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        movementVector.x = floatingJoystick.Horizontal * character.GetMovementSpeed();
        movementVector.y = floatingJoystick.Vertical * character.GetMovementSpeed();   
        rb2D.velocity = movementVector;
    }

}

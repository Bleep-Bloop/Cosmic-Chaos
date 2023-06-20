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
    private SpriteRenderer characterSpriteRenderer;

    private Vector3 movementVector = new Vector3();
    private Vector3 shotDirection = new Vector3(1, 0, 0);


    private void Awake()
    {
        character = GetComponent<CharacterBase>();
        rb2D = GetComponent<Rigidbody2D>();
        characterSpriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Ensure directional weapons are always heading in direction of movement.
        if(movementVector != Vector3.zero)
            shotDirection = movementVector;

        if(movementVector.x < 0)
            characterSpriteRenderer.flipX = false;
        else if(movementVector.x > 0)
            characterSpriteRenderer.flipX = true;

        // Debug
        if (Input.GetKeyDown(KeyCode.X))
            UpgradeManager.instance.OpenUpgradeMenu();
        if(Input.GetKeyDown(KeyCode.C))
            UpgradeManager.instance.CloseUpgradeMenu();
        
    }

    private void Move()
    {
        movementVector.x = floatingJoystick.Horizontal * character.GetMovementSpeed();
        movementVector.y = floatingJoystick.Vertical * character.GetMovementSpeed();   
        rb2D.velocity = movementVector;
    }

    public Vector3 GetShotDirection()
    {
        return shotDirection;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference
    PlayerAnimations playerAnimations;
    PlayerControls playerControls;

    //Movement Variables
    public float playerBaseMovementSpeed; 
    float speedX, speedY;

    //Unity Objects
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = FindObjectOfType<PlayerControls>();
        playerAnimations = transform.Find("Player_Visuals").GetComponent<PlayerAnimations>(); /*Used to find the referenced script if its
        inside a parent GameObject - JC*/
    }

    // Update is called once per frame
    void Update()
    {
        /*Movement Controls sa PC (WASD, OR ARROW UP, DOWN, LEFT, RIGHT)- JC
        speedX = Input.GetAxisRaw("Horizontal") * playerBaseMovementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * playerBaseMovementSpeed;
        rb.velocity = new Vector2(speedX, speedY);*/

        //Joystick Controls sa CP - JC
        Vector2 joystickInput = playerControls.joystickVector;

        // Set the player's velocity based on joystick input
        rb.velocity = joystickInput * playerBaseMovementSpeed;

        //Animation Movements - JC
        Vector2 direction = new Vector2(speedX, speedY); //Take last position - JC
        //playerAnimations.AnimateMovement(direction);
        playerAnimations.AnimateMovement(joystickInput);
    }
}

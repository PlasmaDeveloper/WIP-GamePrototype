using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //objects and componets
    CharacterController playerController;
    Transform groundCheck;      //object to set position for the ground check

    //values
    float playerSpeed = 12.0f;
    float jumpHeight = 3.0f;

    float gravity = -9.81f;     //gravity strenght
    Vector3 velocity;       //current fallspeed

    float groundDistrance = 0.4f;       //size of the area checked for is grounded
    [SerializeField] LayerMask groundMask;       //defines which objects are the ground
    bool playerIsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        SetupScript();
    }

    // Update is called once per frame
    void Update()
    {
        IsPlayerGrounded();
        PlayerMovement();
        PlayerJump();
        PlayerFallDown();
    }

    void SetupScript()
    {
        playerController = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");

    }

    void PlayerMovement()
    {
        float inputAxisX = Input.GetAxis("Horizontal");
        float inputAxisY = Input.GetAxis("Vertical");

        Vector3 movePosition = transform.right * inputAxisX + transform.forward * inputAxisY;
        playerController.Move(movePosition * playerSpeed * Time.deltaTime);
    }

    void PlayerFallDown()
    {
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && playerIsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ResetVelocity()
    {
        if (playerIsGrounded && velocity.y < 0)
        {
            //moves the player still down, just in case it is still a bit up in the air
            velocity.y = -2.0f;
        }
    }

    void IsPlayerGrounded()
    {
        playerIsGrounded = Physics.CheckSphere(groundCheck.position, groundDistrance, groundMask);
        //Debug.Log(playerIsGrounded);
    }
}

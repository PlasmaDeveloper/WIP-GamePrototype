using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //objects and componets
    private CharacterController playerController;
    private Transform groundCheck;      //object to set position for the ground check

    //values
    private float playerSpeed = 12.0f;
    private float runMultiplier = 2.0f; //amount the playerSpeed is multiplied with, when running
    private float jumpHeight = 3.0f;

    private float gravity = -9.81f;     //gravity strenght
    private Vector3 velocity;       //current fallspeed

    private float groundDistrance = 0.4f;       //size of the area checked for is grounded
    [SerializeField] LayerMask groundMask;       //defines which objects are the ground
    private bool playerIsGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        SetupScript();
    }

    // Update is called once per frame
    private void Update()
    {
        IsPlayerGrounded();
        PlayerMovement();
        PlayerJump();
        PlayerFallDown();
    }

    private void SetupScript()
    {
        playerController = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");

    }

    private void PlayerMovement()
    {
        float inputAxisX = Input.GetAxis("Horizontal");
        float inputAxisY = Input.GetAxis("Vertical");

        Vector3 movePosition = transform.right * inputAxisX + transform.forward * inputAxisY;

        //let player run, when pressing shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("Is running");
            playerController.Move(movePosition * (playerSpeed * runMultiplier) * Time.deltaTime);
        }
        else
        {
            playerController.Move(movePosition * playerSpeed * Time.deltaTime);
        }
        
    }

    private void PlayerFallDown()
    {
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && playerIsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ResetVelocity()
    {
        if (playerIsGrounded && velocity.y < 0)
        {
            //moves the player still down, just in case it is still a bit up in the air
            velocity.y = -2.0f;
        }
    }

    private void IsPlayerGrounded()
    {
        playerIsGrounded = Physics.CheckSphere(groundCheck.position, groundDistrance, groundMask);
        //Debug.Log(playerIsGrounded);
    }
}

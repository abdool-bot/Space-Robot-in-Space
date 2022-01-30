using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject turner;
    public GameObject model;
    public float moveSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    [SerializeField] private GameObject gameManager;

    private TimeTaker timeTaker;
    private Vector3 velocity;
    private bool isGrounded = true;

    private void Start()
    {
        if (gameManager != null)
        {
            timeTaker = gameManager.GetComponent<TimeTaker>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Player is able to jump by checking if it is on the ground.
        if (controller.isGrounded)
        {
            velocity.y = -2f;
            isGrounded = true;
        }

        if (!controller.isGrounded)
        {
            isGrounded = false;
        }

        //Keyboard Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        

        //Movement
        Vector3 move = turner.transform.right * moveX + turner.transform.forward * moveZ;
        controller.Move(move * (moveSpeed * Time.deltaTime));


        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;   
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        model.transform.rotation = Quaternion.Euler(turner.transform.eulerAngles.x,turner.transform.eulerAngles.y-90,turner.transform.eulerAngles.z-83);
    }

    private void OnTriggerExit(Collider other)
    {
        if (timeTaker == null)
        {
            return;
        }
        switch (other.tag)
        {
            case "Start":
                timeTaker.StartPassed();
                break;
            case "Finish":
                timeTaker.FinishPassed();
                break;
        }
    }
}
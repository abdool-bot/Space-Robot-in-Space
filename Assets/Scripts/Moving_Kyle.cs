using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moving_Kyle : MonoBehaviour
{
    // Variables to assign for following camera
    [Header("Variables to assign for following camera")]
    [SerializeField]
    private CharacterController cc;
    [SerializeField]
    private GameObject playerModel;
    [SerializeField]
    private GameObject cameraRotation;

    // General variables for movement
    [Header("Movement variables")]
    [SerializeField]
    private float movementSpeed = 8f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 2.5f;

    private float inGameMovementSpeed;

    [Header("")] 
    [SerializeField] 
    private GameObject gameManager;
    
    // Variables for jumping
    private Vector3 jumpVelocity;
    private bool isGrounded;
    
    // Variable to save the standing rotation while not moving
    private Quaternion rotationWhenStanding = Quaternion.identity;
    
    // Time taking script for finish and start
    private TimeTaker timeTaker;

    
    void Start()
    {
        if(gameManager != null) timeTaker = gameManager.GetComponent<TimeTaker>();
        Cursor.lockState = CursorLockMode.Locked;
        inGameMovementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(cc.isGrounded)
        {
            jumpVelocity.y = -5f;
            isGrounded = true;
        }

        if(!cc.isGrounded)
        {
            isGrounded = false;
        }

        // Keyboard Inputs
        float moveHortizonal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            inGameMovementSpeed = movementSpeed / 4;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            inGameMovementSpeed = movementSpeed;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            isGrounded = false;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Movement
        Vector3 move = cameraRotation.transform.right * moveHortizonal + cameraRotation.transform.forward * moveVertical;
        cc.Move(move * (inGameMovementSpeed * Time.deltaTime));

        // Gravity
        jumpVelocity.y += gravity * Time.deltaTime;
        cc.Move(jumpVelocity * Time.deltaTime);

        if (moveHortizonal == 0 && moveVertical == 0)
        {
            playerModel.transform.rotation = rotationWhenStanding;
            return;
        }

        var cameraEuler = cameraRotation.transform.eulerAngles;
        playerModel.transform.rotation = Quaternion.Euler(1- cameraEuler.x, cameraEuler.y, cameraEuler.z);

        rotationWhenStanding = playerModel.transform.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (timeTaker == null) return;
        
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

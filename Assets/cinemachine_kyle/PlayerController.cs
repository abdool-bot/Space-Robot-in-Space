using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference mouseControl;


    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMain;
    private Transform playerRotation;

    private Animator animator;

    private int isRunningHash;
    private int isSneakingHash;
    private int isJumpingHash;

    private void OnEnable() {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        mouseControl.action.Enable();
    }

    private void OnDisable() {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        mouseControl.action.Enable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector2 looking = mouseControl.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        Vector3 look = new Vector3(looking.x, 0, looking.y);

        if(move != Vector3.zero){
            animator.SetBool("isRunning", true);
        }else{
            animator.SetBool("isRunning", false);
        }

        move = cameraMain.forward * move.z + cameraMain.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    /*  Guckt nicht in Richtung der Kamera  */

        if(movement != Vector2.zero){
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    

    /*  Guckt in Richtung der Kamera (WIP/EXPERIMENTAL)  

        if(looking != Vector2.zero){
            Quaternion direction = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime * rotationSpeed);
        }
    */

    }
}


using UnityEngine;
using UnityEngine.InputSystem;

public class cinemachine_animator : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;

    private Animator animator;

    private int isRunningHash;
    private int isSneakingHash;
    private int isJumpingHash;

    private void OnEnable() {
        movementControl.action.Enable();
    }

    private void OnDisable() {
        movementControl.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool fowardPressed = Input.GetKey("w");
        bool sneakPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");

        if (!isRunning && fowardPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !fowardPressed)
        {
            animator.SetBool(isRunningHash, false);
        }
        if (fowardPressed && sneakPressed)
        {
            animator.SetBool(isSneakingHash, true);
        }
        if (fowardPressed && !sneakPressed)
        {
            animator.SetBool(isSneakingHash, false);
        }
        if (jumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (!jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}

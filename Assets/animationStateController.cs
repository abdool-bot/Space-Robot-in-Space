using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Animator animator;

    private int isRunningHash;
    private int isRunningBackHash;
    private int isSneakingHash;
    private int isJumpingHash;
    private int isStrafeRHash;
    private int isStrafeLHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isRunningBackHash = Animator.StringToHash("isRunningBack");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
        isStrafeLHash = Animator.StringToHash("isStrafeLeft");
        isStrafeRHash = Animator.StringToHash("isStrafeRight");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isRunningBack = animator.GetBool("isRunningBack");
        bool isStrafeL = animator.GetBool("isStrafeLeft");
        bool isStrafeR = animator.GetBool("isStrafeRight");
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool sneakPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown("space");
        bool strafeLPressed = Input.GetKey("a");
        bool strafeRPressed = Input.GetKey("d");
        
        //running forward
        if (!isRunning && forwardPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !forwardPressed)
        {
            animator.SetBool(isRunningHash, false);
        }
        //strafe left
        if (!isStrafeL && strafeLPressed)
        {
            animator.SetBool(isStrafeLHash, true);
        }
        if (isStrafeL && !strafeLPressed)
        {
            animator.SetBool(isStrafeLHash, false);
        }//strafe right
        if (!isStrafeR && strafeRPressed)
        {
            animator.SetBool(isStrafeRHash, true);
        }
        if (isStrafeR && !strafeRPressed)
        {
            animator.SetBool(isStrafeRHash, false);
        }
        //running backwards
        if (!isRunningBack && backwardPressed)
        {
            animator.SetBool(isRunningBackHash, true);
        }
        if (isRunningBack && !backwardPressed)
        {
            animator.SetBool(isRunningBackHash, false);
        }
        //sneaking
        if (forwardPressed && sneakPressed)
        {
            animator.SetBool(isSneakingHash, true);
        }
        if ((forwardPressed && !sneakPressed) ||(!forwardPressed && sneakPressed))
        {
            animator.SetBool(isSneakingHash, false);
        }
        //jumping
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

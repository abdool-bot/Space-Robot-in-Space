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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isRunningBackHash = Animator.StringToHash("isRunningBack");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isRunningBack = animator.GetBool("isRunningBack");
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool sneakPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown("space");
        
        //running forward
        if (!isRunning && forwardPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !forwardPressed)
        {
            animator.SetBool(isRunningHash, false);
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
        if (forwardPressed && !sneakPressed)
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

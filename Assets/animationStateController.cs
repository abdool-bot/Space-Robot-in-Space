using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Animator animator;

    private int isRunningHash;
    private int isSneakingHash;
    private int isJumpingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isSneakingHash = Animator.StringToHash("isSneaking");
        isJumpingHash = Animator.StringToHash("isJumping");
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

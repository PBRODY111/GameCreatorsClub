using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isSprinting = animator.GetBool("isSprinting");
        bool isReversing = animator.GetBool("isReversing");

        // walk forwards
        if(!isWalking && (Input.GetKey("w") || Input.GetKey("up"))){
            animator.SetBool("isWalking", true);
        }
        if(isWalking && !(Input.GetKey("w") || Input.GetKey("up"))){
            animator.SetBool("isWalking", false);
        }

        // walk backwards
        if(!isReversing && (Input.GetKey("s") || Input.GetKey("down"))){
            animator.SetBool("isReversing", true);
        }
        if(isReversing && !(Input.GetKey("s") || Input.GetKey("down"))){
            animator.SetBool("isReversing", false);
        }

        // sprint forwards
        if(isWalking && Input.GetKey("left shift")){
            animator.SetBool("isSprinting", true);
        }
        if(!isWalking && !Input.GetKey("left shift")){
            animator.SetBool("isSprinting", false);
        }
        
    }
}

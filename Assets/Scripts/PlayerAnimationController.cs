using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("fire");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.ResetTrigger("fire");
        }
    }
}

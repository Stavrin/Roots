using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private int animSpeed = Animator.StringToHash("Speed"); //create hash id

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void UpdateAnimation(float speed)
    {
        animator.SetFloat(animSpeed, speed);
    }

}

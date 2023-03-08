using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController controller;
    public Animator animator;
    private Vector3 playerVelocity;
    private float playerSpeed = 10f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
    }

    void Update()
    {
        Vector3 move = new Vector3(-joystick.Vertical, 0, joystick.Horizontal);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetTrigger("isWalking");
        }
        if (move == Vector3.zero)
        {
            animator.ResetTrigger("isWalking");
            animator.ResetTrigger("isAttacking");
        }
    }
}
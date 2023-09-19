using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isSprinting;
    private bool isCrouching;
    private bool lerpCrouch;
    private float crouchTimer;
    
    public float motorSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        crouchTimer = 0f;
        lerpCrouch = false;
        isSprinting = false;
        isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (lerpCrouch) { 
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            if (isCrouching)
            {
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            }
            else {
                characterController.height = Mathf.Lerp(characterController.height, 2, p);
            }

            if (p > 1) {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input) {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * motorSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        //if touch the ground, change velocity.y
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump() {
        if (isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch() { 
        isCrouching = !isCrouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint() { 
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            motorSpeed = 8f;
        }
        else {
            motorSpeed = 5f;
        }
    }
}

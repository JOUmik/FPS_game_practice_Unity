using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;

    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();

        onFootActions.Jump.performed += ctx => playerMotor.Jump();
        onFootActions.Crouch.performed += ctx => playerMotor.Crouch();
        onFootActions.Sprint.performed += ctx => playerMotor.Sprint();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        playerMotor.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFootActions.Enable();
    }

    private void OnDisable()
    {
        onFootActions.Disable();
    }
}

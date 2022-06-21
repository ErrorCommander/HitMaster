using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Tap.performed += PrintCoordinateTach;
    }

    private void PrintCoordinateTach(UnityEngine.InputSystem.InputAction.CallbackContext input)
    {
        Debug.Log( input.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}

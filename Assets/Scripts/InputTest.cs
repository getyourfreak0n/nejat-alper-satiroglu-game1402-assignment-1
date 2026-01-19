using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private PlayerInputActions _testActions;

    void Awake()
    {
        _testActions = new PlayerInputActions();
        _testActions.Enable();
    }

    void OnEnable()
    {
        _testActions.Player.Jump.performed += Jump;
        _testActions.Player.Shoot.performed += Shoot;
        
    }

    void OnDisable()
    {
        _testActions.Disable();
        _testActions.Player.Jump.performed -= Jump;
        _testActions.Player.Shoot.performed -= Shoot;
        
    }

    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }

    void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("BANG!!!");
    }
}
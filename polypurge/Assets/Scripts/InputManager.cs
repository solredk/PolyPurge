using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 lookInput;

    private PlayerMovement movement;
    private PlayerLook look;

    [SerializeField] private Gun gun;

    private void Awake()
    {
        DoInitialize();
    }

    public void DoInitialize()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponentInChildren<PlayerLook>();
        gun = GetComponentInChildren<Gun>();
    }

    public void DoJump(InputAction.CallbackContext context)
    {
        if (context.performed && movement != null)
            movement.Jump();
    }

    public void DoSprint(InputAction.CallbackContext context)
    {
        movement?.SetSprint(context.ReadValueAsButton());
    }

    public void DoMoving(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void DoLooking(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    public void DoInteracting(InputAction.CallbackContext context)
    {
        look.Interacting();

    }

    public void DoSwitchWeapon(InputAction.CallbackContext context)
    {
        context.ReadValue<float>();

    }
    private void FixedUpdate()
    {
        if (movement != null)
        {
            movement.ProcessMove(moveInput);
        }
    }

    private void LateUpdate()
    {
        if (look != null)
        {
            look.MyInput(lookInput);
        }
    }
}
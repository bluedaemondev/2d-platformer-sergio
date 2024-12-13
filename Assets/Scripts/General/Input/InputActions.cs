using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(InputSystem))]
public class InputActions : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 movement;
    public bool jump;
    public bool interact;
    public bool switchItem;
    public bool pause;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;


#if ENABLE_INPUT_SYSTEM
    public void OnMovement(InputAction.CallbackContext value)
    {
        MoveInput(value.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpInput(value.phase == InputActionPhase.Performed);
    }


    public void OnPause(InputAction.CallbackContext value)
    {
        PauseInput(value.performed);
    }
    public void OnUse(InputAction.CallbackContext value)
    {
        InteractInput(value.performed);
    }
    public void OnSwitch(InputAction.CallbackContext value)
    {
        SwitchInput(value.performed);
    }
#endif
    public void MoveInput(Vector2 newMoveDirection)
    {
        movement = newMoveDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void PauseInput(bool newPauseState)
    {
        pause = newPauseState;
    }
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }
    public void SwitchInput(bool newSwitchInput)
    {
        switchItem = newSwitchInput;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

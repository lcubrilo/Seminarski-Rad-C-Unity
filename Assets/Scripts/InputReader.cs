using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking {get; private set;}
    public Vector2 MovementValue {get; private set;}
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    //public event Action AttackEvent;
    private Controls controls;
    // Add a state machine here and cause it to change state??? within the onjump?
    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        JumpEvent?.Invoke();
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        DodgeEvent?.Invoke();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context) {}
    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        TargetEvent?.Invoke();
    }
    public void OnCancel(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.canceled){IsAttacking = false;}
        else if(!context.performed){return;}
        
        //AttackEvent?.Invoke();
        IsAttacking = true;
    }
}

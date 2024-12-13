using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public string paramWalking = "Walk";
    public string paramCrouch = "Crouch";
    public string paramJump = "Jump";

    public Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Walk(bool state)
    {
        _animator.SetBool(paramWalking, state);
    }
    public void Crouch(bool state)
    {
        _animator.SetBool(paramCrouch, state);

    }
    public void Jump(bool state)
    {
        _animator.SetBool(paramJump, state);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool appliedForce = false;
    private Attack attack;
    private PlayerBaseState previousState;

    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime); //Aplies forces
        //Rotated in lock-in
        //Debug.Log(GetNormalizedTime());
        //Debug.Log(attack.AttackTime);
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
            if (normalizedTime < attack.ForceTime)
            {
                TryApplyForce(normalizedTime);
            }
        }
        else
        {
            //stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {
        
    }
    private void TryComboAttack(float normalizedTime)
    {
        Debug.Log("Tried combo");
        Debug.Log(attack.ComboIndex);
        
        if (attack.ComboIndex == -1) { return; }
        Debug.Log(normalizedTime);
        if (normalizedTime < attack.AttackTime) { return; }
        
        Debug.Log("Attack is done");
        stateMachine.SwitchState(new PlayerAttackState(stateMachine, attack.ComboIndex));

    }
    private void TryApplyForce(float normalizedTime)
    {
        if(appliedForce){return;}
        appliedForce = true;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
    }
    private float GetNormalizedTime()
    {
        AnimatorStateInfo curr = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo next = stateMachine.Animator.GetNextAnimatorStateInfo(0);
        
        if (stateMachine.Animator.IsInTransition(0) && next.IsTag("Attack"))
        {
            return next.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && curr.IsTag("Attack"))
        {
            return curr.normalizedTime;
        }
        else
        {
            return 0f;
        }

    }
}

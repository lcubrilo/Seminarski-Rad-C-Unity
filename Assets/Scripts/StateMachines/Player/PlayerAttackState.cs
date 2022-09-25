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
    private int index;

    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
        index = attackIndex;
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
            // go back to locotmotion
        }

        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {
        
    }
    private void TryComboAttack(float normalizedTime)
    {
        if (index == -1) { return; }

        if (normalizedTime < attack.AttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackState(stateMachine, index));

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
        
        if(!next.IsTag("Attack")){return 0f;}
        if(stateMachine.Animator.IsInTransition(0)){   return next.normalizedTime;}
        else{return curr.normalizedTime;}
    }
}

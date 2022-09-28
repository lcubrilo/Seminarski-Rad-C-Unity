using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine){ }
    protected readonly int ImpactHash = Animator.StringToHash("Impacted");
    protected readonly int TargettingBlendHash = Animator.StringToHash("TargettingBlendTree");
    private float duration = 1.0f;
    public override void Enter()
    {
        if(stateMachine.health.health > 0)
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
        else
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <= 0)
        {
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}

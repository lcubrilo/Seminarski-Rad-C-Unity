using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine){ this.stateMachine = stateMachine;}
    protected readonly int ImpactHash = Animator.StringToHash("Impacted");
    private float duration = 1.0f;
    public override void Enter()
    {
        if(stateMachine.health.health > 0)
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
        else
            stateMachine.SwitchState(new EnemyDeadState(stateMachine));

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <= 0)
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
    }
    public override void Exit()
    {

    }
}

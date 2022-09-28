using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log("Tick.");
        Move(deltaTime);
        if(IsInSight())
        {
            Debug.Log("See you.");
            //stateMachine.SwitchState()
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, 0.1f, deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
    }
    
}

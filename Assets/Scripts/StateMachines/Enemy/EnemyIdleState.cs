using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){  }
    protected readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected readonly int SpeedHash = Animator.StringToHash("Velocity");
    public override void Enter()
    {
        Debug.Log("Idle");
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
        if(stateMachine.health.healthBar.TryGetComponent<Slider>(out Slider sl))
        {
            sl.value = stateMachine.health.health;
            sl.maxValue = stateMachine.health.maxHealth;
        }
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("Tick.");
        Move(deltaTime);
        if(IsInSight())
        {
            Debug.Log("See you.");
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, 0.1f, deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
    }
    
    
}

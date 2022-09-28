using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine){ this.stateMachine = stateMachine;}

    public override void Enter()
    {
         stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInSight())
        {
            Debug.Log("Don't see you.");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        MoveToPlayer(deltaTime);
        stateMachine.Animator.SetFloat(SpeedHash, 1f, 0.1f, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;

    }
    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination = stateMachine.Player.transform.position;

        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;

    }
}

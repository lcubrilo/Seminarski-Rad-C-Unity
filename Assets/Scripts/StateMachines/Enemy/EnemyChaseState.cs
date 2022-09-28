using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine){ this.stateMachine = stateMachine;}
    protected readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected readonly int SpeedHash = Animator.StringToHash("Velocity");
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine psm);
        psm.spotted();
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInSight())
        {
            Debug.Log("Don't see you.");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        if(IsInReach())
        {
            Debug.Log("I can hit you.");
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            return;
        }
        MoveToPlayer(deltaTime);
        FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash, 1f, 0.1f, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;

    }
    private void MoveToPlayer(float deltaTime)
    {
        Debug.Log("Attempt movement");
        if(stateMachine.Agent.isOnNavMesh){
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;

    }
}

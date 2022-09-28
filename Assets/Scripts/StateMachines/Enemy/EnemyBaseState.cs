using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    protected readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected readonly int SpeedHash = Animator.StringToHash("Speed");
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        //Adding forces like gravity
        this.stateMachine.Controller.Move((motion+this.stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected bool IsInSight()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        //Debug.Log(playerDistanceSqr);
        //Debug.Log(stateMachine.PlayerSight * stateMachine.PlayerSight);
        return playerDistanceSqr <= stateMachine.PlayerSight * stateMachine.PlayerSight;

    }
    protected bool IsInReach()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        //Debug.Log(playerDistanceSqr);
        //Debug.Log(stateMachine.PlayerSight * stateMachine.PlayerSight);
        return playerDistanceSqr <= stateMachine.PlayerReach * stateMachine.PlayerReach;

    }
    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        
    }

}

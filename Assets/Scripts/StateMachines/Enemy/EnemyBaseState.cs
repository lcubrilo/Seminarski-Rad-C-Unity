using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
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
    protected void FacePlayer()
    {
        if(stateMachine.Player == null){return;}
        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
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

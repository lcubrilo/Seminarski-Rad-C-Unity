using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        //Adding forces like gravity
        this.stateMachine.Controller.Move((motion+this.stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FaceTarget()
    {
        Target currTarg = stateMachine.Targeter.CurrentTarget;
        if(currTarg == null){return;}
        Vector3 lookPos = currTarg.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    protected readonly int DiedHash = Animator.StringToHash("Died");
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DiedHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        //Bleed out
    }
    public override void Exit()
    {
       
    }
}

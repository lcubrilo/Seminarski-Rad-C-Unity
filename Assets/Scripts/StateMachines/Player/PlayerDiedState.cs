using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    protected readonly int DiedHash = Animator.StringToHash("Died");
    private float dyingTime = 3.0f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DiedHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        dyingTime -= deltaTime;
        if(dyingTime <= 0)
        {
            Debug.Log("lol noob");
            stateMachine.GUI.SetActive(false);
            stateMachine.DeathScreen.SetActive(true);
            Exit();
        }
    }
    public override void Exit()
    {
       
    }
}

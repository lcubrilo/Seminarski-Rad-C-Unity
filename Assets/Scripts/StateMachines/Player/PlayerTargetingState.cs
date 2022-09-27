using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("ForwardSpeed");
    private readonly int TargetingRightHash = Animator.StringToHash("RightSpeed");
    private const float AnimatorDampTime = 0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, AnimatorDampTime);
    }
    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            Debug.Log("yo");
            //AttackId = (AttackId+1) % stateMachine.Attacks.Length;
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }
        //else{AttackId = -1}

        if(stateMachine.Targeter.CurrentTarget == null){
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement*stateMachine.TargetingVelocity, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }
    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        //Normalize vectors and avoid dividing by zero
        float x = stateMachine.InputReader.MovementValue.x; x = x == 0 ? 0 : x/Mathf.Abs(x);
        float y = stateMachine.InputReader.MovementValue.y; y = y == 0 ? 0 : Mathf.Abs(y)/y;

        stateMachine.Animator.SetFloat(TargetingRightHash, x, AnimatorDampTime, deltaTime);
        stateMachine.Animator.SetFloat(TargetingForwardHash, y, AnimatorDampTime, deltaTime);
    }
}

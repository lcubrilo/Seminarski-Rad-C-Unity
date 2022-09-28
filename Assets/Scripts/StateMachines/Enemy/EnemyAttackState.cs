using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine){ this.stateMachine = stateMachine;}
    protected readonly int AttackHash = Animator.StringToHash("Attack");
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, 0.1f);
        stateMachine.Weapon.SetAttack(20);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine psm);
        psm.attacked();
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInReach())
        {
            Debug.Log("Can't hit you");
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }

    }
    public override void Exit()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerSight { get; private set; }
    [field: SerializeField] public float PlayerReach { get; private set; }
    [field: SerializeField] public WeaponDamage Ouchy { get; private set; }
    [field: SerializeField] public Health health { get; private set; }
    public GameObject Player { get; private set; }

    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        
        SwitchState(new EnemyIdleState(this));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerSight);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PlayerReach);
    }
    private void OnEnable()
    {
        health.OnImpact += HandleTakeDamage;
    }

    private void OnDisnable()
    {
        health.OnImpact += HandleTakeDamage;
    }
    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }
}

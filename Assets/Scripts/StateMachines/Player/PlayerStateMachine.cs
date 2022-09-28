using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public ForceReceiver ForceReceiver {get;private set;}
    [field: SerializeField] public CharacterController Controller {get;private set;}
    [field: SerializeField] public Animator Animator {get;private set;}
    [field: SerializeField] public InputReader InputReader {get;private set;}
    [field: SerializeField] public float FreeLookVelocity {get;private set;}
    [field: SerializeField] public float TargetingVelocity {get;private set;}
    [field: SerializeField] public float RotationDamping {get;private set;}
    [field: SerializeField] public Targeter Targeter {get;private set;}
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public WeaponDamage Ouchy { get; private set; }
    [field: SerializeField] public Health health { get; private set; }
    [field: SerializeField] public GameObject GUI { get; private set; }
    [field: SerializeField] public GameObject DeathScreen { get; private set; }
    
    [SerializeField] public GameObject[] tips;
    private bool everSpotted = false;
    private bool everAttacked = false;
    public void spotted(){
        if(everSpotted) return;
        Debug.Log("PLAYER HAS BEEN SPOTTED");
        if(tips.Length > 1) 
        {
            tips[1].SetActive(true);
            Destroy(tips[1], 5.0f);
        }
        everSpotted = true;
    }
    public void attacked(){
        if(everAttacked) return;
        Debug.Log("PLAYER HAS BEEN ATTACKED");
        if(tips.Length > 2) 
        {
            tips[2].SetActive(true);
            Destroy(tips[2], 5.0f);
        }
        everSpotted = true;
    }
    
    public Transform MainCameraTransform {get; private set;}
    private void Start()
    {
        if(tips.Length > 0)
        {
            tips[0].SetActive(true);
            Destroy(tips[0], 5.0f);
        }
        MainCameraTransform = Camera.main.transform;
        
        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this));
    }
}

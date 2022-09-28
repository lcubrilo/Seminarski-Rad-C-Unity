using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myself;
    [SerializeField] public GameObject healthBar;
    
    public List<Collider> attackedSoFar = new List<Collider>();
    private int damage;

    private void OnEnable()
    {
        attackedSoFar.Clear();
        //attackedSoFar = new List<Collider>();
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == this.myself)
        {
            //Debug.Log("Hit myself");
            return;
        }
        if (attackedSoFar.Contains(other))
        {
            //Debug.Log("Already hit this");
            return;
        }
        
        //Debug.Log(attackedSoFar);
        attackedSoFar.Add(other);
        

        if (other.TryGetComponent<Health>(out Health health))
            //Debug.Log("Deal dmg");
            health.DealDamage(damage);
        
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver fr)){
            //Debug.Log("Deal dmg");
            Vector3 direction = (other.transform.position - myself.transform.position).normalized;

            direction.x *= 0.2f;
            direction.y *= 0.2f;
            direction.z *= 0.2f;
            
            fr.AddForce(direction);
        }
        
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }

}

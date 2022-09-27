using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myself;
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
        {
            //Debug.Log("Deal dmg");
            health.DealDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }

}

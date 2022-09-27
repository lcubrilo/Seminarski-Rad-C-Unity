using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int health;

    private void Start()
    {
        this.health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        //Debug.Log(damage);
        if (health <= 0){return;}
        health -= damage;
        //Debug.Log(health);
    }
}

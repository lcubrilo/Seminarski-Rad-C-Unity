using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public int health;
    [SerializeField] public GameObject healthBar;

    private void Start()
    {
        this.health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        //Debug.Log(damage);
        if (health <= 0){return;}
        health -= damage;
        Debug.Log(health);

        if(healthBar!=null)
        {
            healthBar.TryGetComponent<Slider>(out Slider sl);
            sl.value -= damage;
        }
    }
}

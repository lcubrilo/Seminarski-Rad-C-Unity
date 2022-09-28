using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAfter5min : MonoBehaviour
{
    [SerializeField] public GameObject[] tips;
    private bool everSpotted = false;
    private bool everAttacked = false;
    public void spotted(){
        if(everSpotted) return;
        if(tips.Length > 1) Destroy(tips[1], 5.0f);
        everSpotted = true;
    }
    public void attacked(){
        if(everAttacked) return;
        if(tips.Length > 2) Destroy(tips[2], 5.0f);
        everAttacked = true;
    }
    void Start()
    {
        if(tips.Length > 0) Destroy(tips[0], 5.0f);
    }
    
}

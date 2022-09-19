using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();
    public Target CurrentTarget {get; private set;}
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target t)){return;}
        targets.Add(t);
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target t)){return;}
        targets.Remove(t);
            
    }
    public bool SelectTarget()
    {
        if(targets.Count == 0){return false;}
        CurrentTarget = targets[0];
        return true;
    }
    public void Cancel()
    {
        CurrentTarget = null;
    }
}

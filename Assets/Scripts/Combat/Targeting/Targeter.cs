using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    public List<Target> targets = new List<Target>();
    public Target CurrentTarget {get; private set;}
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target t)){return;}
        targets.Add(t);
        t.OnDestroyed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.TryGetComponent<Target>(out Target t)){return;}
        RemoveTarget(t);
    }
    public bool SelectTarget()
    {
        if(targets.Count == 0){return false;}
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;
        foreach(Target target in targets)
        {
            Vector2 viewpos = mainCamera.WorldToViewportPoint(target.transform.position);
            if(viewpos.x<0||viewpos.x>1||viewpos.y<0||viewpos.y>1){continue;}

            Vector2 toCenter = viewpos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
                // Modified Pythagora's Theorem
            }
        }
        if(closestTarget==null){return false;}

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }
    public void Cancel()
    {
        if(CurrentTarget==null){return;}

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }
    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}

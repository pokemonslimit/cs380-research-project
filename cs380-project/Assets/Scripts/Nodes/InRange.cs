using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : Node
{
    private float range;
    private Transform target;
    private Transform origin;

    public InRange(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        if(target)
        {
            float distance = Vector3.Distance(target.position, origin.position);
            return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
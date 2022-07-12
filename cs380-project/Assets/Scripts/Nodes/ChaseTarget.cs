using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTarget : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private agentAI ai;

    public ChaseTarget(Transform target, NavMeshAgent agent, agentAI ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        //debug drawing
        //ai.SetColor(Color.yellow);
        
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }

    // Does nothing for now,
    // maybe calc based on hunger or something?
    public override void CalcUtility()
    {
    
    }

}
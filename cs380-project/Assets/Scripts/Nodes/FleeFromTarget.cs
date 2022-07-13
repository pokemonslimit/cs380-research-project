using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeFromTarget : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private agentAI ai;
    private float targetdistance;

    public FleeFromTarget(Transform target, NavMeshAgent agent, agentAI ai, float targetdistance)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
        this.targetdistance = targetdistance;
    }

    public override NodeState Evaluate()
    {
        //debug drawing
        //ai.SetColor(Color.yellow);
        
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(distance < targetdistance)
        {
            agent.isStopped = false;
            Vector3 directionToPlayer = agent.transform.position - target.position;
            Vector3 newPosition = agent.transform.position + directionToPlayer;
            agent.SetDestination(newPosition);
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
        float x = ai.utilityBlackboard["Fear"];
        //Quadratic Curves
        UtilityScore = (x / 100.0f) * (x / 100.0f);
    }

}
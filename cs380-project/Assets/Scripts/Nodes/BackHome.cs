using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackHome : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private agentAI ai;

    public BackHome(Transform target, NavMeshAgent agent, agentAI ai)
    {
        name = "Back Home";
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        //debug drawing
        //ai.SetColor(Color.yellow);
        ai.currentNode = this;
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
        UtilityScore = 0.2f;
    }

}
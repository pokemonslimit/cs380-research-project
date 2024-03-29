using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : Node
{
    private NavMeshAgent agent;
    private agentAI ai;
    private float range;
    public Idle(NavMeshAgent agent, agentAI ai)
    {
        name = "Idle";
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.currentNode = this;
        //debug drawing
        agent.isStopped = true;
        float mood = Random.Range(0.01f,0.1f);
        ai.utilityBlackboard["Mood"] += mood;
        return NodeState.SUCCESS;
    }

    public override void CalcUtility()
    {
        UtilityScore = 0.3f;
    }
}
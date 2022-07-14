using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roaming : Node
{
    private NavMeshAgent agent;
    private agentAI ai;
    private float range;
    public Roaming(NavMeshAgent agent, agentAI ai,float range)
    {
        name = "Roaming";
        this.agent = agent;
        this.ai = ai;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        //debug drawing
        ai.SetColor(Color.yellow);
        ai.currentNode = this;
        float distance = Vector3.Distance(RandomLocation(range), agent.transform.position);
        if(distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(RandomLocation(range));
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            ai.utilityBlackboard["Mood"] -= 0.1f;
            return NodeState.SUCCESS;
        }
    }

     public Vector3 RandomLocation(float radius) {
         Vector3 randomDirection = Random.insideUnitSphere * radius;
         randomDirection += agent.transform.position;
         NavMeshHit hit;
         Vector3 finalPosition = Vector3.zero;
         if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
             finalPosition = hit.position;            
         }
         return finalPosition;
     }

    public override void CalcUtility()
    {
        float x = ai.utilityBlackboard["Mood"];
        UtilityScore = x / 100.0f;
    }
}
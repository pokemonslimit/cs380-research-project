using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchForFood : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private agentAI ai;
    private string foodtarget;

    public SearchForFood(NavMeshAgent agent, agentAI ai,string foodtarget)
    {
        name = "Search For Food";
        this.agent = agent;
        this.ai = ai;
        this.foodtarget = foodtarget;
    }

    public override NodeState Evaluate()
    {
        //debug drawing
        ai.currentNode = this;
        ai.SetColor(Color.red);
        float distance = 0.0f;
        GameObject temp = FindClosestFood(foodtarget);
        if(temp)
        {
            target = temp.transform;
            distance = Vector3.Distance(target.position, agent.transform.position);
        }
        else
        {
            Debug.Log("No game objects are tagged with");
            Debug.Log(foodtarget);
        }

        //Debug.Log("distance = ");
        //Debug.Log(distance);
        if(distance > 5.0f)
        {
            agent.isStopped = false;
            //Debug.Log("finding food");
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("get food");
            ai.utilityBlackboard["Hunger"] = 100.0f;
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }

    // Does nothing for now,
    // maybe calc based on hunger or something?
    public override void CalcUtility()
    {
        float _hunger = ai.utilityBlackboard["Hunger"];
        UtilityScore = CustomHungerPLCFunction(_hunger);
    }

    public float CustomHungerPLCFunction(float x)
    {
        //PCL = Piecewise Linear Curve;
        //x = current value, m = max value;
        if(x <= 15.0f)
        {
            return 1.0f;
        }
        else if(x > 15.0f && x <= 30.0f)
        {
            return 0.75f;
        }
        else if(x > 30.0f && x <= 45.0f)
        {
            return 0.25f;
        }
        else if(x > 45.0f && x <= 60.0f)
        {
            return 0.1f;
        }
        else
        {
            return 0.0f;
        }
    }

    public GameObject FindClosestFood(string food)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(food);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = agent.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
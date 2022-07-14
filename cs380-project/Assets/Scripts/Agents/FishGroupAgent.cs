using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishGroupAgent : agentAI
{
    [SerializeField] GameObject predator;
    [SerializeField] float predatorDetectRange;
    [SerializeField] float roamingRange;
    [SerializeField] string food;
    public override void ConstructBehahaviourTree()
    {
        //Leaf
        Idle idle = new Idle(agent, this);
        Roaming roaming = new Roaming(agent, this, roamingRange);
        SearchForFood searchforfood = new SearchForFood(agent,this,food);
        FleeFromTarget fleefromtarger = new FleeFromTarget(predator.transform, agent, this, predatorDetectRange);
        //Root
        topNode = new UtilitySelector(new List<Node>{idle, roaming, searchforfood, fleefromtarger }, this);


    }
    public override void ConstructBlackBoard()
    {
        // 5 attributes
        // Health, if Health is zero, agent destroy
        // Hunger, if Hunger is zero, health will decrease per frame.
        // Fear, if shark is detected, called flee.
        // Growth, change the size of agent.
        // breeding, to max value, will spwan new agent.

        //0 in list 
        utilityBlackboard.Add("Health",100.0f);
        // relative behaviors: 
        //die (re-spwan)

        utilityBlackboard.Add("Hunger", 100.0f);
        // relative behaviors: 
        //search for food

        utilityBlackboard.Add("Mood", 0.0f);

        utilityBlackboard.Add("Fear", 0.0f);
        // relative behaviors:
        // flee

    }


    // Update is called once per frame
    public override void Update()
    {
        topNode.CalcUtility();
        topNode.Evaluate();

        if(utilityBlackboard["Health"] <= 0.0f)
        {
            //die and respawn
            RespawnInNewPosisiton(500.0f);
            utilityBlackboard["Health"] = 100.0f;
        }

        if (utilityBlackboard["Hunger"] > 0.0f)
        {
            utilityBlackboard["Hunger"] -= 0.01f;
        }

        if (utilityBlackboard["Hunger"] <= 0.0f)
        {
            utilityBlackboard["Health"] -= 0.01f;
        }

        if(DetectPedator(predatorDetectRange))
        {
            utilityBlackboard["Fear"] += 1.0f;
        }
        else
        {
            utilityBlackboard["Fear"] = 0.0f;
        }

    }

    public void RespawnInNewPosisiton(float range)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, 1))
        {
            finalPosition = hit.position;
        }
        transform.position = finalPosition;
    }

    public bool DetectPedator(float range)
    {
        if (predator)
        {
            float distance = Vector3.Distance(predator.transform.position, transform.position);
            return distance <= range ? true : false;
        }
        else
        {
            return false;
        }
    }


}

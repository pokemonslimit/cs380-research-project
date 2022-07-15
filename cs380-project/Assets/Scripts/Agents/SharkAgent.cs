using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkAgent : agentAI
{
    [SerializeField] GameObject hideout;
    [SerializeField] float roamingRange;
    [SerializeField] string food;
    public override void ConstructBehahaviourTree()
    {
        // base.ConstructBehahaviourTree();
        BackHome backtohideout = new BackHome(hideout.transform, agent, this);
        SearchForFood searchforfood = new SearchForFood(agent, this, food);

        //Root
        topNode = new UtilitySelector(new List<Node> { backtohideout, searchforfood}, this);
    }
    public override void ConstructBlackBoard()
    {
        // base.ConstructBlackBoard();
        utilityBlackboard.Add("Health", 100.0f);
        //make sure the blackboard order is same as all agent.
        //it is useless now 

        utilityBlackboard.Add("Hunger", 100.0f);
        //relative behavior:
        //search for food

    }


    // Update is called once per frame
    public override void Update()
    {
        topNode.CalcUtility();
        topNode.Evaluate();
        if (utilityBlackboard["Hunger"] > 0.0f)
        {
            utilityBlackboard["Hunger"] -= 0.005f;
        }
    }
}

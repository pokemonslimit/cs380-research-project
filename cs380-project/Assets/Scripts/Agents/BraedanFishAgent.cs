using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BraedanFishAgent : agentAI
{
    public override void ConstructBehahaviourTree()
    {
        // base.ConstructBehahaviourTree();
    }
    public override void ConstructBlackBoard()
    {
        // base.ConstructBlackBoard();
        
        //utilityBlackboard.Add(new Tuple<string, float>("Healh", 55.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Hunger", 20.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Fear", 0.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Environment", 50.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Social", 50.0f));


        //utilityBlackboard.Add(new Tuple<string, float>("Mood", 100.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Max Social", 100.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Max Healh", 100.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Max Hunger", 100.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Max Fear", 100.0f));
        //utilityBlackboard.Add(new Tuple<string, float>("Max Environment", 100.0f));
    }
    // Update is called once per frame
    public override void Update()
    {
        topNode.CalcUtility();
        topNode.Evaluate();
    }
}

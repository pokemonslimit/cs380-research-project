using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlankFishAgent : agentAI
{
    public override void ConstructBehahaviourTree()
    {
        // base.ConstructBehahaviourTree();

    }
    public override void ConstructBlackBoard()
    {
       // base.ConstructBlackBoard();


    }


    // Update is called once per frame
    public override void Update()
    {
        topNode.CalcUtility();
        topNode.Evaluate();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentAI : MonoBehaviour
{

    protected Material material;
    protected NavMeshAgent agent;
    protected Node topNode;
    //protected List<Tuple<string, float>> utilityBlackboard = new List<Tuple<string, float>>();
    public Dictionary<string, float> utilityBlackboard = new Dictionary<string, float>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        ConstructBlackBoard();
        ConstructBehahaviourTree();
    }

    public virtual void ConstructBlackBoard()
    {
        // Just random stuff for testing
    }

    public virtual void ConstructBehahaviourTree()
    {
        //Leaf create
        Roaming roamingNode = new Roaming(agent,this,100.0f);
        //Decorator

        //Root
        topNode = new Selector(new List<Node> { roamingNode });
    }

    public virtual void Update()
    { 
        topNode.CalcUtility();
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            agent.isStopped = true;
        }
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

}
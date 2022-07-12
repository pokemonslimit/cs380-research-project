using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentAI : MonoBehaviour
{

    [SerializeField] private float _chasingRange;
  
    [SerializeField] private Transform _target;

    protected Material material;
    protected NavMeshAgent agent;
    protected Node topNode;
    protected List<Tuple<string, float>> utilityBlackboard = new List<Tuple<string, float>>();

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
        utilityBlackboard.Add(new Tuple<string, float>("Healh", 55.0f));
        utilityBlackboard.Add(new Tuple<string, float>("Energy", 23.0f));
    }

    public virtual void ConstructBehahaviourTree()
    {
        //Leaf create
        Roaming roamingNode = new Roaming(agent,this);
        InRange rangeDetectNode = new InRange(_chasingRange, _target, transform);
        ChaseTarget chaseNode = new ChaseTarget(_target, agent, this);
        //Decorator
        Inverter invertNode = new Inverter( rangeDetectNode);
        Inverter invertNode2 = new Inverter( roamingNode);

        //Root
        topNode = new Selector(new List<Node> {invertNode2, invertNode, chaseNode});
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
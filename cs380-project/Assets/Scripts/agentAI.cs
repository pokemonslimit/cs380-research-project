using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentAI : MonoBehaviour
{

    [SerializeField] private float _chasingRange;
  
    [SerializeField] private Transform _target;

    private Material material;
    private NavMeshAgent agent;
    private Node topNode;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        //Leaf create
        InRange rangeDetectNode = new InRange(_chasingRange, _target, transform);
        ChaseTarget chaseNode = new ChaseTarget(_target, agent, this);
        //Decorator
        Inverter invertNode = new Inverter( rangeDetectNode);

        //Root
        topNode = new Selector(new List<Node> {invertNode, chaseNode});
    }

    private void Update()
    {
        topNode.Evaluate();
        if(topNode.nodeState == NodeState.FAILURE)
        {
            agent.isStopped = true;
        }
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitySequencer : Node
{
    protected List<Node> nodes = new List<Node>();
    private agentAI ai;
    public UtilitySequencer(List<Node> nodes, agentAI ai)
    {
        name = "Utility Sequencer";
        this.ai = ai;
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        ai.currentNode = this;
        bool isAnyNodeRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }

    // Sorts the children nodes based on Utility for Selector/Sequencers
    // Sorts highest to lowest
    // Sets own utility == to highest utility in child nodes,
    // Not sure that's the best thing to do, but good enough to get it working
    public override void CalcUtility()
    {
        foreach (Node node in nodes)
            node.CalcUtility();

        nodes.Sort(delegate (Node x, Node y)
        {
            if (x.UtilityScore > y.UtilityScore)
                return -1;
            else if (x.UtilityScore < y.UtilityScore)
                return 1;
            else
                return 0;
        });

        UtilityScore = nodes[0].UtilityScore;
    }
}

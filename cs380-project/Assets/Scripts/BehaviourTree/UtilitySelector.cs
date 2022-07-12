using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitySelector : Node
{
    protected List<Node> nodes = new List<Node>();

    public UtilitySelector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        _nodeState = NodeState.FAILURE;
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

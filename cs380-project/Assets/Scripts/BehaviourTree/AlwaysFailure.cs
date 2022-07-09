using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFailure : Node
{
	protected Node node;

	public AlwaysFailure(Node node)
	{
		this.node = node;
	}
	public override NodeState Evaluate()
	{
		switch (node.Evaluate())
		{
			case NodeState.RUNNING:
				_nodeState = NodeState.RUNNING;
				
				break;
			case NodeState.SUCCESS:
				_nodeState = NodeState.FAILURE;
				break;
			case NodeState.FAILURE:
				_nodeState = NodeState.FAILURE;
				break;
			default:
				break;
		}
		return _nodeState;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
	protected NodeState _nodeState;

	protected float utilityScore_; 

	public NodeState nodeState { get { return _nodeState; } }
	public float UtilityScore
	{
		get { return utilityScore_; }
		set { utilityScore_ = value; }
	}

	public abstract NodeState Evaluate();

	public abstract void CalcUtility();

}

public enum NodeState
{
	RUNNING, SUCCESS, FAILURE,
}
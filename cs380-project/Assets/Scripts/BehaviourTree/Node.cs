using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
	protected NodeState _nodeState;
	protected string name;
	protected float utilityScore_; 

	public string Name { get { return name; } }
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
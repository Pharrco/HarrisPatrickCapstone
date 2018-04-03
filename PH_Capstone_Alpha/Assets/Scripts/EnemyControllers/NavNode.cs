using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode
{
	public Vector2Int Position { get; protected set; }
	public List<NavNode> neighbors;

	public NavNode(Vector2Int n_pos)
	{
		// Store the position
		Position = n_pos;

		// Create the list of neighbors
		neighbors = new List<NavNode>();
	}

	public override bool Equals(object obj)
	{
		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType())
			return false;

		NavNode p = (NavNode)obj;
		return (Position == p.Position);
	}
}

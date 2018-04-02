using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavNode
{
	public Vector2Int Position { get; protected set; }
	public List<NavNode> neighbors;
	public float Distance { get; protected set; }
	public NavNode Last { get; protected set; }

	public NavNode(Vector2Int n_pos)
	{
		// Store the position
		Position = n_pos;

		// Create the list of neighbors
		neighbors = new List<NavNode>();
	}

	public void TestPath(NavNode prev, float n_distance, List<NavNode> goals)
	{
		float hold_distance = n_distance;

		List<float> goal_dists = new List<float>();

		foreach (NavNode n_goal in goals)
		{
			goal_dists.Add(Vector2Int.Distance(Position, n_goal.Position));
		}

		float goal_distance = Mathf.Min(goal_dists.ToArray());

		hold_distance += goal_distance;

		if (hold_distance < Distance)
		{
			Last = prev;

			Distance = hold_distance;
		}
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

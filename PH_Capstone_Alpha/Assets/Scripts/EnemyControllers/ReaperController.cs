using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : BaseEnemyController {

	private enum BehaviorState { Idle, Dormant, Hunting, Blinded }

	List<NavNode> node_network = new List<NavNode>();
	NavNode curr_node, move_node, attack_node;

	BehaviorState curr_state = BehaviorState.Idle;

	List<Vector2Int> available_moves = new List<Vector2Int> {
		new Vector2Int( 1 , 2 ),
		new Vector2Int( -1 , -2 ),
		new Vector2Int( 1 , -2 ),
		new Vector2Int( -1 , 2 ),
		new Vector2Int( 2 , 1 ),
		new Vector2Int( -2 , -1 ),
		new Vector2Int( 2 , -1 ),
		new Vector2Int( -2 , 1 )
	};

	List<Vector2Int> prime2_moves = new List<Vector2Int> {
		new Vector2Int( 2 , 2 ),
		new Vector2Int( 0 , 2 ),
		new Vector2Int( -2 , 2 ),
		new Vector2Int( 2 , 0 ),
		new Vector2Int( -2 , 0 ),
		new Vector2Int( 2 , -2 ),
		new Vector2Int( 0 , -2 ),
		new Vector2Int( -2 , -2 )
	};

	List<Vector2Int> prime1_moves = new List<Vector2Int> {
		new Vector2Int( 1 , 3 ),
		new Vector2Int( -1 , -3 ),
		new Vector2Int( 1 , -3 ),
		new Vector2Int( -1 , 3 ),
		new Vector2Int( 3 , 1 ),
		new Vector2Int( -3 , -1 ),
		new Vector2Int( 3 , -1 ),
		new Vector2Int( -3 , 1 )
	};

	public override void PlayerOn()
	{
		EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
	}

	public override void LightEffect(LightStatus n_lightColor)
	{
		LightStatus m_lightColor;

		if ( (n_lightColor == LightStatus.Dark) || (n_lightColor == LightStatus.Nopwr) )
		{
			m_lightColor = LightResourceControl.Player_LightStatus;
		}
		else
		{
			m_lightColor = n_lightColor;
		}

		switch (m_lightColor)
		{
			case LightStatus.White:
				curr_state = BehaviorState.Hunting;
				break;
			case LightStatus.Infrd:
				curr_state = BehaviorState.Dormant;
				break;
			case LightStatus.Ulvlt:
				curr_state = BehaviorState.Blinded;
				break;
		}
	}

	public override void GetMove()
	{

	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	private void BuildNodeNetwork(Vector2Int start_point)
	{
		List<NavNode> prep_queue = new List<NavNode>();
		prep_queue.Add(new NavNode(start_point));
		curr_node = prep_queue[0];

		while (prep_queue.Count > 0)
		{
			NavNode ip_node = prep_queue[0];
			prep_queue.RemoveAt(0);

			foreach (Vector2Int poss_move in available_moves)
			{
				Vector2Int temp_pos = ip_node.Position + poss_move;

				if ((Mathf.Clamp(temp_pos.x, 0, BuildBoard.GetArrayHeight()) == temp_pos.x) && (Mathf.Clamp(temp_pos.y, 0, BuildBoard.GetArrayWidth()) == temp_pos.y))
				{
					if (BuildBoard.GetArrayValue(temp_pos.x, temp_pos.y) > 0)
					{
						if (prep_queue.Contains(new NavNode(temp_pos)))
						{
							ip_node.neighbors.Add(prep_queue.Find(el => el.Position == temp_pos));
						}
						else if (node_network.Contains(new NavNode(temp_pos)))
						{
							ip_node.neighbors.Add(node_network.Find(el => el.Position == temp_pos));
						}
						else
						{
							NavNode n_node = new NavNode(temp_pos);
							ip_node.neighbors.Add(n_node);
							prep_queue.Add(n_node);
						}
					}
				}
			}

			node_network.Add(ip_node);
		}
	}

	private NavNode GetBestMove()
	{
		// Create a temp node based on the player's position
		NavNode player_node = new NavNode(new Vector2Int(PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y));

		// Test if the player node is one of the current node's neighbors
		if (curr_node.neighbors.Contains(player_node))
		{
			// if the player node is one of the current node's neighbors, return the player's position
			return curr_node.neighbors.Find(el => el.Position == player_node.Position);
		}

		// Create the list of possible moves to be evaluated between each phase
		List<NavNode> goal_list = new List<NavNode>();

		// Test for 2prime moves in neighbors
		foreach (Vector2Int n_move in prime2_moves)
		{
			// If a node with this coordinate exists in the list of neighbors
			if (curr_node.neighbors.Find(el => el.Position == (player_node.Position + n_move)) != null)
			{
				// Add this move to the list of possible moves
				goal_list.Add(curr_node.neighbors.Find(el => el.Position == (player_node.Position + n_move)));
			}
		}

		// If possible moves have been identified, select one
		if (goal_list.Count > 0)
		{
			if (goal_list.Count > 1)
			{
				return goal_list[Random.Range(0, goal_list.Count)];
			}
			else
			{
				return goal_list[0];
			}
		}

		// Test for 1prime moves in neighbors
		foreach (Vector2Int n_move in prime1_moves)
		{
			// If a node with this coordinate exists in the list of neighbors
			if (curr_node.neighbors.Find(el => el.Position == (player_node.Position + n_move)) != null)
			{
				// Add this move to the list of possible moves
				goal_list.Add(curr_node.neighbors.Find(el => el.Position == (player_node.Position + n_move)));
			}
		}

		// If possible moves have been identified, select one
		if (goal_list.Count > 0)
		{
			if (goal_list.Count > 1)
			{
				return goal_list[Random.Range(0, goal_list.Count)];
			}
			else
			{
				return goal_list[0];
			}
		}

		// Set null best moves
		NavNode bestMove = null;
		float min_dist = 100;

		// Test all neighbor nodes
		foreach (NavNode neighb in curr_node.neighbors)
		{
			// If node is closer than current min
			if (Vector2Int.Distance(neighb.Position, player_node.Position) < min_dist)
			{
				// Store the neighbor node as best move, store the distance
				bestMove = neighb;
				min_dist = Vector2Int.Distance(neighb.Position, player_node.Position);
			}
		}

		// If a move has been found
		if (bestMove != null)
		{
			// Return the best move
			return bestMove;
		}
		else
		{
			// Otherwise, return no move
			return new NavNode(new Vector2Int(-1, -1));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : BaseEnemyController {

	List<NavNode> node_network = new List<NavNode>();
	NavNode curr_node;
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

	}

	public override void LightEffect(LightStatus n_lightColor)
	{

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
		NavNode player_node = new NavNode(new Vector2Int(PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y));

		if (curr_node.neighbors.Contains(player_node))
		{
			return curr_node.neighbors.Find(el => el.Position == player_node.Position);
		}

		List<NavNode> goal_list = new List<NavNode>();

		// TODO: Start work here on 2' neighbors


		// REMOVE
		return new NavNode(Vector2Int.zero);
	}

	private Vector2 GetBestPathMove()
	{
		return Vector2.zero;
	}
}

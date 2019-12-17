using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_24 : LevelBase
{
	public LevelDat_1_24()
	{
		Level_array = new int[,] {
			{ 5, 4, 3, 3, 4, 5 },
			{ 5, 4, 0, 0, 4, 5 },
			{ 0, 3, 2, 2, 3, 0 },
			{ 1, 2, 0, 0, 2, 1 },
			{ 1, 2, 3, 3, 2, 1 },
			{ 2, 3, 0, 0, 3, 2 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(0, 2),
			new Vector2(0, 3),
			new Vector2(0, 4),
			new Vector2(0, 5),
			new Vector2(2, 2),
			new Vector2(2, 3),
			new Vector2(3, 0),
			new Vector2(3, 1),
			new Vector2(4, 2),
			new Vector2(4, 3),
			new Vector2(3, 4),
			new Vector2(3, 5),
			new Vector2(5, 0),
			new Vector2(5, 1),
			new Vector2(5, 4),
			new Vector2(5, 5)
		};

		Empty_list = new List<Vector2>()
		{
			
		};

		Player_start = new Vector2(1, 4);

		Facing_start = 180;

		Level_id = 25;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{

		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			new GorgonDirectionalSpawn(Enumerators.CompassDirect.North)
		};
	}

}

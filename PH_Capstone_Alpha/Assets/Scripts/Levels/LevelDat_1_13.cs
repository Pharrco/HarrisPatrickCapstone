using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_13 : LevelBase
{
	public LevelDat_1_13()
	{
		Level_array = new int[,] {
		{ 2, 0, 3, 3, 2, 3 },
		{ 3, 0, 2, 0, 0, 3 },
		{ 2, 0, 0, 0, 5, 4 },
		{ 2, 1, 4, 3, 5, 4 },
		{ 0, 2, 3, 2, 1, 3 },
		{ 0, 0, 4, 3, 2, 2 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(2, 5),
			new Vector2(3, 3),
			new Vector2(3, 5),
			new Vector2(4, 2),
			new Vector2(4, 4),
			new Vector2(5, 3),
		};

		Empty_list = new List<Vector2>() {
			new Vector2(1, 2),
		};

		Player_start = new Vector2(1, 2);

		Facing_start = 180;

		Level_id = 13;
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
			new GorgonDirectionalSpawn(Enumerators.CompassDirect.East)
		};
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_11 : LevelBase
{
	public LevelDat_1_11()
	{
		Level_array = new int[,] {
		{ 1, 2, 3, 4, 3, 2 },
		{ 1, 4, 4, 0, 4, 0 },
		{ 1, 0, 3, 3, 4, 4 },
		{ 2, 2, 3, 3, 0, 3 },
		{ 0, 2, 0, 4, 4, 2 },
		{ 2, 3, 3, 3, 0, 2 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>() {
			new Vector2(1, 1),
			new Vector2(1, 2),
			new Vector2(4, 4),
			new Vector2(4, 3),
			new Vector2(3, 2),
			new Vector2(3, 3),
			new Vector2(2, 2),
			new Vector2(2, 3),
			new Vector2(5, 5)
		};

		Player_start = new Vector2(5, 5);

		Facing_start = 180;

		Level_id = 12;
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
			new SimpleSlimeSpawner( 3, 1, 1, 1, 2),
			new SimpleSlimeSpawner( 3, 4, 4, 4, 3)
		};
	}

}

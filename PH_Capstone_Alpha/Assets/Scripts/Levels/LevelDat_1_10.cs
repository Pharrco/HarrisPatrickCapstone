using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_10 : LevelBase
{
	public LevelDat_1_10()
	{
		Level_array = new int[,] {
		{ 2, 2, 1, 1, 2, 3 },
		{ 3, 3, 0, 0, 0, 4 },
		{ 3, 4, 3, 2, 0, 3 },
		{ 4, 4, 0, 0, 0, 2 },
		{ 5, 4, 4, 4, 0, 2 },
		{ 4, 5, 4, 3, 2, 1 }
		};

		Marker_list = new List<Vector2>() {
			
		};

		Empty_list = new List<Vector2>() {
			new Vector2(0, 5),
			new Vector2(4, 1),
			new Vector2(4, 2),
			new Vector2(4, 3),
			new Vector2(3, 1),
			new Vector2(2, 1),
			new Vector2(2, 2),
			new Vector2(2, 3)
		};

		Player_start = new Vector2(0, 5);

		Facing_start = 180;

		Level_id = 10;
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
			new SimpleSlimeSpawner( 3, 4, 3, 4, 2),
		};
	}

}
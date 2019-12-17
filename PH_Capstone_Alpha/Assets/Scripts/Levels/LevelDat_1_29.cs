using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_29 : LevelBase
{
	public LevelDat_1_29()
	{
		Level_array = new int[,] {
			{ 5, 5, 5, 1, 1, 1 },
			{ 1, 5, 1, 1, 5, 1 },
			{ 1, 1, 1, 1, 1, 5 },
			{ 1, 1, 1, 1, 1, 5 },
			{ 5, 5, 1, 1, 5, 0 },
			{ 5, 1, 1, 1, 5, 0 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(0, 2),
			new Vector2(1, 1),
			new Vector2(1, 4),
			new Vector2(2, 5),
			new Vector2(3, 5),
			new Vector2(4, 0),
			new Vector2(4, 1),
			new Vector2(4, 4),
			new Vector2(5, 0),
			new Vector2(5, 4)
		};

		Player_start = new Vector2(2, 2);

		Facing_start = 180;

		Level_id = 30;
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
			new DragonBoss(3)
		};
	}

}

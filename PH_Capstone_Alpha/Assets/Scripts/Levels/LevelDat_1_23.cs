using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_23 : LevelBase
{
	[SerializeField]
	GameObject true_chest, false_chest;

	public LevelDat_1_23()
	{
		Level_array = new int[,] {
			{ 3, 2, 2, 3, 3, 3 },
			{ 3, 5, 2, 2, 1, 4 },
			{ 3, 4, 1, 5, 4, 4 },
			{ 4, 4, 5, 1, 4, 3 },
			{ 4, 1, 2, 2, 5, 3 },
			{ 3, 3, 3, 2, 2, 3 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(2, 2),
			new Vector2(3, 3),
			new Vector2(3, 0),
			new Vector2(2, 5)
		};

		Player_start = new Vector2(4, 4);

		Facing_start = 180;

		Level_id = 24;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 2, 2, true_chest, Vector3.zero, 0),
			new EnemySpawn( 3, 3, true_chest, Vector3.zero, 0),
			new EnemySpawn( 3, 0, false_chest, Vector3.zero, 0),
			new EnemySpawn( 2, 5, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{

		};
	}

}

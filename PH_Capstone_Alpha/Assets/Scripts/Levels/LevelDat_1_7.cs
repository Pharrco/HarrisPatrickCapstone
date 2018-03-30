using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_7 : LevelBase
{
	[SerializeField]
	GameObject true_chest, false_chest;

	public LevelDat_1_7()
	{
		Level_array = new int[,] {
		{ 2, 3, 4, 4, 3, 2 },
		{ 3, 0, 3, 0, 0, 3 },
		{ 4, 0, 4, 3, 0, 4 },
		{ 5, 0, 5, 3, 0, 4 },
		{ 5, 0, 0, 2, 0, 3 },
		{ 4, 3, 2, 3, 3, 4 }
		};

		Marker_list = new List<Vector2>() {
			
		};

		Empty_list = new List<Vector2>() {
			new Vector2(0, 5),
			new Vector2(5, 0),
			new Vector2(5, 5),
			new Vector2(0, 0),
			new Vector2(2, 3)
		};

		Player_start = new Vector2(0, 5);

		Facing_start = 90;

		Level_id = 7;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 0, 0, true_chest, Vector3.zero, 0),
			new EnemySpawn( 5, 5, true_chest, Vector3.zero, 0),
			new EnemySpawn( 5, 0, false_chest, Vector3.zero, 0),
			new EnemySpawn( 2, 3, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{

		};
	}

}


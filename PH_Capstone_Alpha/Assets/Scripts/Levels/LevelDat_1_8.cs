using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_8 : LevelBase
{
	[SerializeField]
	GameObject true_chest, false_chest;

	public LevelDat_1_8()
	{
		Level_array = new int[,] {
		{ 4, 3, 2, 3, 4, 3 },
		{ 3, 0, 0, 0, 5, 4 },
		{ 4, 0, 3, 4, 4, 3 },
		{ 4, 0, 0, 5, 0, 2 },
		{ 5, 0, 3, 4, 0, 1 },
		{ 4, 4, 3, 3, 2, 1 }
		};

		Marker_list = new List<Vector2>() {

		};

		Empty_list = new List<Vector2>() {
			new Vector2(0, 5),
			new Vector2(2, 2),
			new Vector2(4, 2),
			new Vector2(5, 5)
		};

		Player_start = new Vector2(0, 5);

		Facing_start = 90;

		Level_id = 9;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 4, 2, true_chest, Vector3.zero, 0),
			new EnemySpawn( 5, 5, false_chest, Vector3.zero, 0),
			new EnemySpawn( 2, 2, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{

		};
	}

}



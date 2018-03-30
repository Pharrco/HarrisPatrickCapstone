using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_6 : LevelBase
{
	[SerializeField]
	GameObject true_chest, false_chest;

	public LevelDat_1_6()
	{
		Level_array = new int[,] {
		{ 2, 1, 1, 2, 1, 2 },
		{ 0, 0, 2, 0, 0, 3 },
		{ 5, 4, 3, 3, 0, 4 },
		{ 4, 0, 3, 3, 4, 5 },
		{ 3, 0, 0, 2, 0, 0 },
		{ 2, 3, 4, 3, 2, 1 }
		};

		Marker_list = new List<Vector2>() {
			new Vector2(1, 2),
			new Vector2(2, 1),
			new Vector2(3, 4),
			new Vector2(4, 3)
		};

		Empty_list = new List<Vector2>() {
			new Vector2(2, 3),
			new Vector2(0, 5),
			new Vector2(5, 0)

		};

		Player_start = new Vector2(2, 3);

		Facing_start = 270;

		Level_id = 6;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{
			
		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 0, 5, true_chest, Vector3.zero, 0),
			new EnemySpawn( 5, 0, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			
		};
	}

}


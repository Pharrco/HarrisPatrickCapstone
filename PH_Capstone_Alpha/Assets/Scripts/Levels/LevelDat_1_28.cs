using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_28 : LevelBase
{
	[SerializeField]
	GameObject false_chest;

	public LevelDat_1_28()
	{
		Level_array = new int[,] {
			{ 5, 5, 4, 3, 3, 3 },
			{ 2, 3, 4, 3, 1, 2 },
			{ 1, 0, 5, 2, 0, 5 },
			{ 1, 0, 5, 2, 0, 4 },
			{ 2, 3, 4, 3, 4, 4 },
			{ 5, 5, 5, 4, 0, 0 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(3, 2),
			new Vector2(3, 3)
		};

		Player_start = new Vector2(1, 3);

		Facing_start = 180;

		Level_id = 29;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 3, 2, false_chest, Vector3.zero, 0),
			new EnemySpawn( 3, 3, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			
		};
	}

}

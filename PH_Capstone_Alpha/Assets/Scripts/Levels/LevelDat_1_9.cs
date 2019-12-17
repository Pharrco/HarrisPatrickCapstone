using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_9 : LevelBase
{
	[SerializeField]
	GameObject true_chest;

	public LevelDat_1_9()
	{
		Level_array = new int[,] {
		{ 4, 3, 4, 3, 4, 3 },
		{ 3, 4, 0, 4, 3, 4 },
		{ 4, 3, 0, 0, 0, 3 },
		{ 3, 0, 0, 0, 3, 4 },
		{ 4, 3, 4, 0, 4, 3 },
		{ 3, 4, 3, 4, 3, 4 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(4, 0),
			new Vector2(3, 0),
			new Vector2(2, 0),
			new Vector2(1, 0),
			new Vector2(0, 0),
			new Vector2(4, 1),
			new Vector2(5, 1),
			new Vector2(2, 1),
			new Vector2(1, 1),
			new Vector2(0, 1),
			new Vector2(5, 2),
			new Vector2(4, 2),
			new Vector2(0, 2),
			new Vector2(5, 3),
			new Vector2(1, 3),
			new Vector2(4, 4),
			new Vector2(3, 4),
			new Vector2(5, 4),
			new Vector2(1, 4),
			new Vector2(0, 4),
			new Vector2(4, 5),
			new Vector2(3, 5),
			new Vector2(2, 5),
			new Vector2(1, 5),
			new Vector2(0, 5),
			new Vector2(5, 5)
		};

		Empty_list = new List<Vector2>() {
			new Vector2(0, 3),
			new Vector2(5, 0)
		};

		Player_start = new Vector2(0, 3);

		Facing_start = 90;

		Level_id = 10;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 5, 0, true_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{

		};
	}

}

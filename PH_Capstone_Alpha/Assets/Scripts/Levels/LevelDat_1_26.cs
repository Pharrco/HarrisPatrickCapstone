using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_26 : LevelBase
{
	[SerializeField]
	GameObject reaper;

	public LevelDat_1_26()
	{
		Level_array = new int[,] {
			{ 0, 1, 1, 2, 0, 5 },
			{ 2, 1, 2, 2, 3, 4 },
			{ 3, 1, 1, 2, 1, 4 },
			{ 2, 1, 0, 2, 3, 4 },
			{ 0, 1, 1, 2, 1, 5 },
			{ 0, 2, 1, 3, 3, 4 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			
		};

		Player_start = new Vector2(2, 3);

		Facing_start = 180;

		Level_id = 27;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 0, 5, reaper, new Vector3(0, 0.1f, 0), 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			
		};
	}

}

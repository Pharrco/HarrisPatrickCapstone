using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_27 : LevelBase
{
	[SerializeField]
	GameObject reaper;

	public LevelDat_1_27()
	{
		Level_array = new int[,] {
			{ 3, 2, 3, 0, 4, 5 },
			{ 2, 1, 2, 3, 3, 4 },
			{ 0, 0, 2, 0, 4, 0 },
			{ 2, 1, 2, 3, 3, 4 },
			{ 3, 2, 3, 0, 4, 5 },
			{ 4, 4, 4, 3, 3, 0 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(1, 3),
			new Vector2(2, 4),
			new Vector2(2, 2),
			new Vector2(3, 3)
		};

		Empty_list = new List<Vector2>()
		{
			
		};

		Player_start = new Vector2(4, 5);

		Facing_start = 180;

		Level_id = 28;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 0, 1, reaper, new Vector3(0, 0.1f, 0), 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			
		};
	}

}

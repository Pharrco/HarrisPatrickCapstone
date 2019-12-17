using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_22 : LevelBase
{
	public LevelDat_1_22()
	{
		Level_array = new int[,] {
		{ 5, 5, 5, 5, 5, 5 },
		{ 5, 3, 3, 3, 4, 5 },
		{ 5, 3, 1, 2, 3, 4 },
		{ 4, 3, 1, 2, 3, 4 },
		{ 5, 3, 3, 3, 4, 5 },
		{ 5, 5, 5, 5, 5, 5 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(2, 0),
			new Vector2(3, 0)
		};

		Player_start = new Vector2(3, 2);

		Facing_start = 180;

		Level_id = 23;
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
			new SwitchSlimeSpawner( 3, 2, 0, 3, 0, false, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(1, 4, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(4, 4, false, Enumerators.SwitchColorType.Red)
		};
	}

}

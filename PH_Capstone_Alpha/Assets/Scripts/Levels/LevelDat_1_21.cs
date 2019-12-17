using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_21 : LevelBase
{
	[SerializeField]
	GameObject true_chest;

	public LevelDat_1_21()
	{
		Level_array = new int[,] {
		{ 2, 2, 3, 3, 2, 2 },
		{ 2, 4, 2, 2, 4, 2 },
		{ 3, 2, 2, 2, 2, 3 },
		{ 3, 2, 1, 2, 2, 3 },
		{ 2, 4, 2, 2, 4, 2 },
		{ 2, 2, 3, 3, 2, 2 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(1, 1),
			new Vector2(1, 4),
			new Vector2(4, 1),
			new Vector2(4, 4),
			new Vector2(3, 2),
			new Vector2(3, 3),
			new Vector2(5, 0),
			new Vector2(0, 5)
		};

		Player_start = new Vector2(2, 2);

		Facing_start = 180;

		Level_id = 22;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 0, 5, true_chest, Vector3.zero, 0),
			new EnemySpawn( 5, 0, true_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			new SwitchSlimeSpawner( 3, 3, 2, 3, 3, false, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(1, 1, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(1, 4, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(4, 1, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(4, 4, false, Enumerators.SwitchColorType.Red)
		};
	}

}

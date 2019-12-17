using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_15 : LevelBase
{
	[SerializeField]
	GameObject true_chest, false_chest;

	public LevelDat_1_15()
	{
		Level_array = new int[,] {
		{ 3, 4, 5, 4, 3, 2 },
		{ 4, 3, 0, 0, 4, 3 },
		{ 3, 2, 0, 2, 3, 3 },
		{ 4, 3, 2, 0, 4, 3 },
		{ 3, 3, 0, 0, 3, 2 },
		{ 4, 4, 5, 5, 4, 3 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(0, 0),
			new Vector2(0, 5),
			new Vector2(1, 1),
			new Vector2(1, 4),
			new Vector2(2, 0),
			new Vector2(3, 4),
			new Vector2(4, 0),
			new Vector2(4, 5),
			new Vector2(5, 1),
			new Vector2(5, 4),
			new Vector2(2, 5)
		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(3, 2),
			new Vector2(3, 3)
		};

		Player_start = new Vector2(0, 2);

		Facing_start = 0;

		Level_id = 16;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 3, 2, true_chest, Vector3.zero, 0),
			new EnemySpawn( 2, 3, false_chest, Vector3.zero, 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			new GorgonDirectionalSpawn(Enumerators.CompassDirect.North),
			new GorgonDirectionalSpawn(Enumerators.CompassDirect.South)
		};
	}

}

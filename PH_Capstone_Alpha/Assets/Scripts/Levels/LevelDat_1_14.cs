using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_14 : LevelBase
{
	public LevelDat_1_14()
	{
		Level_array = new int[,] {
		{ 2, 3, 2, 3, 0, 0 },
		{ 3, 4, 0, 4, 5, 0 },
		{ 0, 0, 0, 4, 5, 0 },
		{ 2, 1, 2, 3, 0, 0 },
		{ 3, 2, 0, 4, 3, 0 },
		{ 0, 3, 4, 5, 4, 5 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(1, 3),
			new Vector2(2, 3),
			new Vector2(4, 1),
			new Vector2(4, 3)
		};

		Empty_list = new List<Vector2>() {

		};

		Player_start = new Vector2(0, 0);

		Facing_start = 0;

		Level_id = 15;
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
			new GorgonDirectionalSpawn(Enumerators.CompassDirect.North)
		};
	}

}

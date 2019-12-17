using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_17 : LevelBase
{
	public LevelDat_1_17()
	{
		Level_array = new int[,] {
		{ 1, 1, 1, 1, 2, 3 },
		{ 0, 5, 4, 4, 5, 3 },
		{ 0, 4, 3, 3, 4, 3 },
		{ 3, 4, 3, 3, 4, 0 },
		{ 3, 5, 4, 4, 5, 0 },
		{ 3, 2, 1, 1, 1, 1 }
		};

		Marker_list = new List<Vector2>()
		{
			new Vector2( 1, 1),
			new Vector2( 1, 2),
			new Vector2( 1, 3),
			new Vector2( 1, 4),
			new Vector2( 2, 4),
			new Vector2( 3, 4),
			new Vector2( 4, 4),
			new Vector2( 4, 3),
			new Vector2( 4, 2),
			new Vector2( 4, 1),
			new Vector2( 3, 1),
			new Vector2( 2, 1)
		};

		Empty_list = new List<Vector2>()
		{

		};

		Player_start = new Vector2(5, 5);

		Facing_start = 180;

		Level_id = 18;
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

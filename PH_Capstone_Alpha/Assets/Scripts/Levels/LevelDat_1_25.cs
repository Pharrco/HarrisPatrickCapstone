using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_25 : LevelBase
{
	[SerializeField]
	GameObject reaper;

	public LevelDat_1_25()
	{
		tutorial_text = "Beware of reapers. Their movements may seem unpredictable, but really they're just pieces on the witch's board.";

		Level_array = new int[,] {
			{ 5, 4, 3, 2, 3, 4 },
			{ 4, 4, 3, 3, 2, 3 },
			{ 3, 3, 3, 2, 3, 2 },
			{ 2, 3, 2, 3, 3, 3 },
			{ 3, 2, 3, 3, 4, 4 },
			{ 4, 3, 2, 3, 4, 5 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			
		};

		Player_start = new Vector2(1, 1);

		Facing_start = 180;

		Level_id = 26;
	}

	public void Awake()
	{
		Prop_list = new List<InanimateSpawn>()
		{

		};

		Enemy_Spawn_List = new List<EnemySpawn>()
		{
			new EnemySpawn( 4, 4, reaper, new Vector3(0, 0.1f, 0), 0)
		};

		Environment_Effect_List = new List<EnvironmentEffect>()
		{
			
		};
	}

}

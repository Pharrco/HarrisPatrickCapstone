using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_30 : LevelBase
{
	public LevelDat_1_30()
	{
		//tutorial_text = "Some hazards are controlled by switches. Hazards controlled by red switches only count down on turns when the switch is lit by white or UV light.";

		Level_array = new int[,] {
		//{ 0, 2, 0, 3, 4, 3 },
		//{ 0, 3, 3, 3, 2, 3 },
		//{ 0, 3, 5, 3, 0, 0 },
		//{ 2, 3, 3, 3, 4, 0 },
		//{ 1, 2, 0, 4, 5, 4 },
		//{ 0, 3, 2, 1, 2, 3 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			//new Vector2(2, 2),
			//new Vector2(0, 4),
			//new Vector2(1, 4)
		};

		Player_start = new Vector2(4, 4);

		Facing_start = 180;

		Level_id = 31;
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
			//new SwitchSlimeSpawner( 3, 0, 4, 1, 4, false, false, Enumerators.SwitchColorType.Red),
			//new SwitchLight(2, 2, false, Enumerators.SwitchColorType.Red)
		};
	}

}

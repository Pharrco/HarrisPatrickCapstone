using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_20 : LevelBase
{
	public LevelDat_1_20()
	{
		//tutorial_text = "Some hazards are controlled by switches. Hazards controlled by red switches only count down on turns when the switch is lit by white or UV light.";

		Level_array = new int[,] {
		{ 1, 1, 0, 0, 0, 0 },
		{ 0, 2, 5, 4, 3, 5 },
		{ 0, 3, 5, 3, 2, 0 },
		{ 0, 4, 5, 4, 3, 0 },
		{ 5, 3, 2, 3, 2, 0 },
		{ 0, 0, 0, 0, 5, 0 }
		};

		Marker_list = new List<Vector2>()
		{

		};

		Empty_list = new List<Vector2>()
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 5),
			new Vector2(5, 4),
			new Vector2(4, 0)
		};

		Player_start = new Vector2(1, 1);

		Facing_start = 90;

		Level_id = 21;
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
			new SwitchSlimeSpawner( 3, 0, 0, 0, 1, false, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(1, 5, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(5, 4, false, Enumerators.SwitchColorType.Red),
			new SwitchLight(4, 0, false, Enumerators.SwitchColorType.Red)
		};
	}

}

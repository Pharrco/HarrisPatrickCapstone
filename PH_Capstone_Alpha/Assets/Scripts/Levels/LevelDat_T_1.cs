using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_T_1 : LevelBase {

    [SerializeField]
    GameObject tutorial_marker;
    [SerializeField]
    GameObject true_chest, false_chest, reaper;

    public LevelDat_T_1()
    {
		tutorial_text = "ABCDEF";

        Level_array = new int[,] {
        { 2, 2, 2, 2 },
        { 2, 5, 2, 2 },
        { 5, 2, 2, 5 },
        { 2, 2, 5, 2 },
		{ 5, 2, 2, 2 },
		{ 2, 5, 2, 5 }
		};

        Marker_list = new List<Vector2>() {
            new Vector2(3, 0),
        };

        Empty_list = new List<Vector2>() {
            new Vector2(2, 0),
            new Vector2(1, 1),
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(0, 2)
		};

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 1;
    }

    public void Awake()
    {
        Prop_list = new List<InanimateSpawn>()
        {
            new InanimateSpawn( 0, 0, tutorial_marker, 3.0f)
        };

        Enemy_Spawn_List = new List<EnemySpawn>()
        {
   //         new EnemySpawn( 2, 0, true_chest, Vector3.zero, 0),
   //         new EnemySpawn( 1, 1, false_chest, Vector3.zero, 0),
			//new EnemySpawn( 2, 2, reaper, new Vector3(0, 0.1f, 0), 0)
        };

        Environment_Effect_List = new List<EnvironmentEffect>()
        {
			new DragonBoss(3)
			//new SwitchSlimeSpawner( 3, 0, 0, 0, 1, false, false, Enumerators.SwitchColorType.Red),
			//new GorgonDirectionalSpawn(Enumerators.CompassDirect.South),
			//new SwitchLight(0, 3, false, Enumerators.SwitchColorType.Red)
        };
    }

}

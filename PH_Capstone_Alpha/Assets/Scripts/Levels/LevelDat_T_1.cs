using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_T_1 : LevelBase {

    [SerializeField]
    GameObject tutorial_marker;
    [SerializeField]
    GameObject true_chest, false_chest;

    public LevelDat_T_1()
    {
        Level_array = new int[,] {
        { 3, 3, 3, 4 },
        { 3, 4, 2, 1 },
        { 3, 3, 3, 2 },
        { 3, 4, 3, 3 },
        };

        Marker_list = new List<Vector2>() {
            new Vector2(3, 0),
        };

        Empty_list = new List<Vector2>() {
            new Vector2(2, 0),
            new Vector2(1, 1),
			new Vector2(0, 0)
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
            new EnemySpawn( 2, 0, true_chest, Vector3.zero, 0),
            new EnemySpawn( 1, 1, false_chest, Vector3.zero, 0)
        };

        Environment_Effect_List = new List<EnvironmentEffect>()
        {
            new SimpleSlimeSpawner( 3, 0, 0, 0, 1)
        };
    }

}

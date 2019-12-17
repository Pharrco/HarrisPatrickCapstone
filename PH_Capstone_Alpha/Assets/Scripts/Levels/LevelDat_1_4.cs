using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_4 : LevelBase
{
    [SerializeField]
    GameObject true_chest;

    public LevelDat_1_4()
    {
		tutorial_text = "Earn money by capturing treasure chests.";

		Level_array = new int[,] {
        { 1, 2, 3, 4 },
        { 2, 3, 0, 3 },
        { 3, 0, 0, 2 },
        { 4, 0, 0, 1 },
        };

        Marker_list = new List<Vector2>() {
            
        };

        Empty_list = new List<Vector2>() {
            new Vector2(1, 1)
        };

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 5;
    }

    public void Awake()
    {
        Prop_list = new List<InanimateSpawn>()
        {
        };

        Enemy_Spawn_List = new List<EnemySpawn>()
        {
            new EnemySpawn( 1, 1, true_chest, Vector3.zero, 0)
        };
    }
}

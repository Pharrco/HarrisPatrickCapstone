using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineDat_1_1 : LevelBase
{

    public CineDat_1_1()
    {
        Level_array = new int[,] {
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        };

        Marker_list = new List<Vector2>() {
            new Vector2(3, 0),
        };

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_4 : LevelBase
{

    public LevelDat_1_4()
    {
        Level_array = new int[,] {
        { 1, 2, 3, 4 },
        { 2, 0, 0, 3 },
        { 3, 0, 0, 2 },
        { 4, 0, 0, 1 },
        };

        Marker_list = new List<Vector2>() {
            new Vector2(3, 0),
        };

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 4;
    }

}

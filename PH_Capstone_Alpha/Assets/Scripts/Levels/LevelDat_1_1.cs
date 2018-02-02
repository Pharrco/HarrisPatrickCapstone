using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_1 : LevelBase {

    public LevelDat_1_1()
    {
        Level_array = new int[,] {
        { 5, 5, 5, 5 },
        { 5, 5, 5, 5 },
        { 5, 5, 5, 5 },
        { 5, 5, 5, 5 },
        };

        Marker_list = new List<Vector2>() {
            new Vector2(0, 1),
            new Vector2(0, 2),
            new Vector2(0, 3),
            new Vector2(3, 1),
            new Vector2(3, 2),
            new Vector2(3, 3)
        };

        Player_start = new Vector2(2, 2);

        Facing_start = 90;
    }

}

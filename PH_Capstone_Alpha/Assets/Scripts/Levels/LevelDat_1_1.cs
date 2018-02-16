﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_1 : LevelBase {

    public LevelDat_1_1()
    {
        Level_array = new int[,] {
        { 3, 3, 3, 0 },
        { 3, 0, 3, 3 },
        { 3, 0, 0, 3 },
        { 3, 0, 0, 3 },
        };

        Marker_list = new List<Vector2>() {
        };

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 1;
    }

}

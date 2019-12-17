using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDat_1_2 : LevelBase {

    public LevelDat_1_2()
    {
		tutorial_text = "You can only move to spaces that are no more than one level higher or lower than your current position.";

		Level_array = new int[,] {
        { 2, 1, 2, 3 },
        { 3, 4, 0, 3 },
        { 2, 3, 0, 2 },
        { 1, 1, 0, 1 },
        };

        Marker_list = new List<Vector2>() {
        };

        Player_start = new Vector2(3, 3);

        Facing_start = 270;

        Level_id = 3;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator {

    public static int Player_Pos_X { get; private set; }
    public static int Player_Pos_Y { get; private set; }

    public static void SetPlayerCoord(int n_coord_x, int n_coord_y)
    {
        // If received coordinate is valid for current level
        if ((n_coord_x == Mathf.Clamp(n_coord_x, 0, BuildBoard.GetArrayHeight())) && (n_coord_y == Mathf.Clamp(n_coord_y, 0, BuildBoard.GetArrayWidth())))
        {
            // Set the coordinates
            Player_Pos_X = n_coord_x;
            Player_Pos_Y = n_coord_y;
        }
    }
}

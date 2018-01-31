using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoard : MonoBehaviour {

    [SerializeField]
    GameObject block_white, block_grey, block_top;
    static int[,] board_array;

	// Use this for initialization
	void Awake () {

        // Test array holding the board to be built
        // [Column (Left to Right), Row (Front to Back)]
        board_array = new int[,] { { 1, 2, 1, 2, 3, 2, 3, 4, 5, 4 } , { 2, 1, 2, 1, 2, 1, 2, 3, 4, 5 } , { 3, 4, 5, 4, 3, 4, 1, 4, 5, 4 }, { 2, 3, 2, 5, 2, 3, 2, 5, 2, 3 }, { 1, 4, 3, 4, 3, 4, 3, 4, 3, 2 }, { 2, 5, 4, 3, 4, 4, 4, 1, 2, 1 }, { 3, 4, 3, 2, 3, 4, 5, 2, 1, 2 }, { 2, 3, 4, 3, 2, 5, 5, 5, 2, 1 }, { 1, 0, 4, 4, 3, 4, 5, 4, 1, 2 }, { 2, 3, 4, 5, 4, 5, 4, 3, 2, 1 } };

        // For each column
        for (int i = 0; i < board_array.GetLength(0); i++)
        {
            // For each item in the column
            for (int j = 0; j < board_array.GetLength(1); j++)
            {
                // For the height in the specified cell
                for (int k = 0; k < board_array[i, j]; k++)
                {
                    // If even
                    if (k % 2 == 0)
                    {
                        // Instantiate wall block A
                        GameObject.Instantiate(block_white, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, k, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                    }
                    // If odd
                    else
                    {
                        // Instantiate wall block B
                        GameObject.Instantiate(block_grey, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, k, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                    }
                }

                // If the height in the grid cell is greater than zero
                if (board_array[i, j] > 0)
                {
                    // Instantiate a top surface
                    GameObject.Instantiate(block_top, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, board_array[i, j] - 1, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                }
            }
        }
    }
	
    // Get the height stored in the board atrray
    public static int GetArrayValue(int x, int y)
    {
        return board_array[x, y];
    }

    // Get the total height of the level array
    public static int GetArrayHeight()
    {
        return board_array.GetLength(0);
    }

    // Get the total width of the level array
    public static int GetArrayWidth()
    {
        return board_array.GetLength(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCinematic : MonoBehaviour {

    [SerializeField]
    GameObject block_white, block_grey, block_top;
    static int[,] board_array;
	Dictionary<int, GameObject> style_dictionary;
	[SerializeField]
	GameObject tile_grass, tile_path, tile_asphault;

	// Use this for initialization
	void Awake()
    {

        // Test array holding the board to be built
        // [Column (Left to Right), Row (Front to Back)]
        if (GetComponent<CinematicBase>() != null)
        {
            board_array = GetComponent<CinematicBase>().Level_array;
        }
        else
        {
            board_array = new int[,] { { 1, 1, 2, 1 }, { 3, 2, 3, 1 }, { 4, 3, 0, 2 }, { 4, 4, 3, 3 } };
        }

        // For each column
        for (int i = 0; i < board_array.GetLength(0); i++)
        {
            // For each item in the column
            for (int j = 0; j < board_array.GetLength(1); j++)
            {
                //// For the height in the specified cell
                //for (int k = 0; k < board_array[i, j]; k++)
                //{
                //    // If even
                //    if (k % 2 == 0)
                //    {
                //        // Instantiate wall block A
                //        GameObject.Instantiate(block_white, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, k, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                //    }
                //    // If odd
                //    else
                //    {
                //        // Instantiate wall block B
                //        GameObject.Instantiate(block_grey, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, k, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                //    }
                //}


				style_dictionary = new Dictionary<int, GameObject>();

				style_dictionary.Add(0, tile_grass);
				style_dictionary.Add(1, tile_path);
				style_dictionary.Add(2, tile_asphault);

				// If the height in the grid cell is greater than zero
				if (board_array[i, j] > 0)
                {
					int index = GetComponent<CinematicBase>().Style_array[i, j];

					// Instantiate a top surface
					GameObject.Instantiate(style_dictionary[index], new Vector3((i - (board_array.GetLength(0) / 2)) * 5, board_array[i, j] - 1.2f, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.Euler(-90, 0, 180));
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

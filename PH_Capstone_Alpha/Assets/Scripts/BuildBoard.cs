using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoard : MonoBehaviour {

    [SerializeField]
    GameObject block_white, block_grey, block_top, level_bridge, stair_up;
    [SerializeField]
    GameObject whiteLight_effect;
    static int[,] board_array;

	// Use this for initialization
	void Awake () {

        // Test array holding the board to be built
        // [Column (Left to Right), Row (Front to Back)]
        if (GetComponent<LevelBase>() != null)
        {
            board_array = GetComponent<LevelBase>().Level_array;
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

        // For each column
        for (int i = 1; i < board_array.GetLength(0); i++)
        {
            // For each item in the column
            for (int j = 0; j < board_array.GetLength(1); j++)
            {
                if ((board_array[i,j] == board_array[i - 1, j]) && (board_array[i, j] > 0))
                {
                    // Instantiate a bridge
                    GameObject.Instantiate(level_bridge, new Vector3((i - ((1f + board_array.GetLength(0)) / 2)) * 5, board_array[i, j] - 1, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                }

                if ((board_array[i, j] - board_array[i - 1, j] == 1) && (board_array[i, j] > 0) && (board_array[i - 1, j] > 0))
                {
                    // Instantiate a stair up
                    GameObject.Instantiate(stair_up, new Vector3((i - ((1f + board_array.GetLength(0)) / 2)) * 5, board_array[i, j] - 2.05f, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.identity);
                }

                if ((board_array[i, j] - board_array[i - 1, j] == -1) && (board_array[i, j] > 0) && (board_array[i - 1, j] > 0))
                {
                    // Instantiate a stair down
                    GameObject.Instantiate(stair_up, new Vector3((i - ((1f + board_array.GetLength(0)) / 2)) * 5, board_array[i, j] - 1.05f, (j - (board_array.GetLength(1) / 2)) * 5), Quaternion.Euler(0, 180, 0));
                }
            }
        }

        // For each column
        for (int i = 0; i < board_array.GetLength(0); i++)
        {
            // For each item in the column
            for (int j = 1; j < board_array.GetLength(1); j++)
            {
                if ((board_array[i, j] == board_array[i, j - 1]) && (board_array[i, j] > 0))
                {
                    // Instantiate a top surface
                    GameObject.Instantiate(level_bridge, new Vector3((i - (board_array.GetLength(0)) / 2) * 5, board_array[i, j] - 1, (j - (( 1f + board_array.GetLength(1)) / 2)) * 5), Quaternion.Euler(0,90,0));
                }
                if ((board_array[i, j] - board_array[i, j - 1] == 1) && (board_array[i, j] > 0) && (board_array[i, j - 1] > 0))
                {
                    // Instantiate a stair up
                    GameObject.Instantiate(stair_up, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, board_array[i, j] - 2.05f, (j - (( 1f + board_array.GetLength(1)) / 2)) * 5), Quaternion.Euler(0, 270, 0));
                }

                if ((board_array[i, j] - board_array[i, j - 1] == -1) && (board_array[i, j] > 0) && (board_array[i, j - 1] > 0))
                {
                    // Instantiate a stair down
                    GameObject.Instantiate(stair_up, new Vector3((i - (board_array.GetLength(0) / 2)) * 5, board_array[i, j] - 1.05f, (j - ((1f + board_array.GetLength(1)) / 2)) * 5), Quaternion.Euler(0, 90, 0));
                }
            }
        }

        // Initialize the enemy grid
        EnemyGridControl.InitializeGrid();
    }

    private void Start()
    {
        // If the list exists
        if (GetComponent<LevelBase>().Prop_list != null)
        {
            foreach (InanimateSpawn prop_spawn in GetComponent<LevelBase>().Prop_list)
            {
                GameObject.Instantiate(prop_spawn.Spawn_prefab, new Vector3((prop_spawn.X_coord - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(prop_spawn.X_coord, prop_spawn.Y_coord) - 1f + prop_spawn.H_offset, (prop_spawn.Y_coord - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);
            }
        }

        if (GetComponent<LevelBase>().Enemy_Spawn_List != null)
        {
            foreach (EnemySpawn enemy_spawn in GetComponent<LevelBase>().Enemy_Spawn_List)
            {
                EnemyGridControl.EnemyAdd(enemy_spawn.TriggerSpawn(), enemy_spawn.GetX(), enemy_spawn.GetY());
            }
        }
        else
        {
            Debug.Log("No list detected");
        }

        // Initialize the light grid
        LightEffectControl.InitializeLightGrid(whiteLight_effect);
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

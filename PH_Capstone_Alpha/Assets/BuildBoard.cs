using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoard : MonoBehaviour {

    [SerializeField]
    GameObject block_white, block_grey;
    int[,] board_array;

	// Use this for initialization
	void Awake () {

        // Test array holding the board to be built
        // [Column (Left to Right), Row (Front to Back)]
        board_array = new int[,] { { 1, 2, 1, 2, 3, 2, 3, 4, 5, 4 } , { 2, 1, 2, 1, 2, 1, 2, 3, 4, 5 } , { 3, 4, 5, 4, 3, 4, 1, 4, 5, 4 }, { 2, 3, 2, 5, 2, 3, 2, 5, 2, 3 }, { 1, 4, 3, 4, 3, 4, 3, 4, 3, 2 }, { 2, 5, 4, 3, 4, 4, 4, 1, 2, 1 }, { 3, 4, 3, 2, 3, 4, 5, 2, 1, 2 }, { 2, 3, 4, 3, 2, 5, 5, 5, 2, 1 }, { 1, 0, 4, 4, 3, 4, 5, 4, 1, 2 }, { 2, 3, 4, 5, 4, 5, 4, 3, 2, 1 } };

        for (int i = 0; i < board_array.GetLength(0); i++)
        {
            for (int j = 0; j < board_array.GetLength(1); j++)
            {
                for (int k = 0; k < board_array[i,j]; k++)
                {
                    if (k % 2 == 0)
                    {
                        GameObject.Instantiate(block_white, new Vector3(i - (board_array.GetLength(0) / 2), k, j - (board_array.GetLength(1) / 2)), Quaternion.identity);
                    }
                    else
                    {
                        GameObject.Instantiate(block_grey, new Vector3(i - (board_array.GetLength(0) / 2), k, j - (board_array.GetLength(1) / 2)), Quaternion.identity);
                    }
                }
            }
        }
    }
	
    public int GetArrayValue(int x, int y)
    {
        return board_array[x, y];
    }

    public int GetArrayHeight()
    {
        return board_array.GetLength(0);
    }

    public int GetArrayWidth()
    {
        return board_array.GetLength(1);
    }
}

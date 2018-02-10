using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerControl : MonoBehaviour {

    [SerializeField]
    GameObject n_active_marker, n_inactive_marker, n_simple_marker;
    static GameObject active_marker, inactive_marker, simple_marker;
    static int[,] board_marker_array;
    static GameObject[,] marker_array;
    [SerializeField]
    Text n_markers_text;
    static Text markers_text;
    static int markers_total, markers_passed;

    // Use this for initialization
    public void Start ( ) {
        // Make serialized objects to static
        active_marker = n_active_marker;
        simple_marker = n_simple_marker;
        inactive_marker = n_inactive_marker;
        markers_text = n_markers_text;

        // Reset marker counts
        markers_passed = 0;
        markers_total = 0;

        // Initialize the integer and object array
        board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        marker_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each entry in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                board_marker_array[i, j] = -1;
            }
        }

        foreach (Vector2 coordinate in GetComponent<LevelBase>().Marker_list)
        {
            board_marker_array[(int)coordinate.x, (int)coordinate.y] = 0;
        }

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each entry in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                // If space has not been designated as empty or special
                if (board_marker_array[i, j] == 0)
                {
                    // Add to the total pre-marker count
                    markers_total += 1;

                    // Instantiate the pre-marker
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
                }
                // If space has not been designated as empty or special
                else if (board_marker_array[i, j] == -1)
                {
                    if ((BuildBoard.GetArrayValue(i, j) > 0) && !((i == GetComponent<LevelBase>().Player_start.x) && (j == GetComponent<LevelBase>().Player_start.y)))
                    {
                        // Add to the total pre-marker count
                        markers_total += 1;

                        // Instantiate the pre-marker
                        marker_array[i, j] = GameObject.Instantiate(simple_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
                    }
                }
            }
        }

        // Update the marker text
        markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
    }

	
    // Mark the specified space, showing that a player has passed through the space and is no longer allowed to enter this space
	public static void MarkSpace(int x, int y)
    {
        // If space was occupied by a pre-marker
        if (board_marker_array[x,y] == 0)
        {
            // Add to the marked space total
            markers_passed += 1;

            // Update the marker text
            markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");

            // Destroy the pre-marker
            GameObject.Destroy(marker_array[x, y]);

            // Update the int array to show the space has been marked
            board_marker_array[x, y] = 1;

            // Instantiate the marker
            marker_array[x,y] = GameObject.Instantiate(active_marker, new Vector3((x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(x, y) - 1f,( y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

            // Test for victory
            PhaseController.TestVictory();
        }

        if ((board_marker_array[x, y] == -1) && (marker_array[x,y] != null))
        {
            // Add to the marked space total
            markers_passed += 1;

            // Update the marker text
            markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");

            // Destroy the pre-marker
            GameObject.Destroy(marker_array[x, y]);

            // Test for victory
            PhaseController.TestVictory();
        }
    }

    // Returns whether the player can move to the space based on the marker state. Returns false if a space has been marked.
    public static bool CanMove(int x, int y)
    {
        if (board_marker_array[x,y] <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Reset the level
    public void ResetMarkers( )
    {
        // Reset marker counts
        markers_passed = 0;
        markers_total = 0;

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each entry in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                if (marker_array[i,j] != null)
                {
                    GameObject.Destroy(marker_array[i, j]);
                }
            }
        }

        // Initialize the integer and object array
        board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        marker_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each entry in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                board_marker_array[i, j] = -1;
            }
        }

        foreach (Vector2 coordinate in GetComponent<LevelBase>().Marker_list)
        {
            board_marker_array[(int)coordinate.x, (int)coordinate.y] = 0;
        }

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each entry in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                // If space has not been designated as empty or special
                if (board_marker_array[i, j] == 0)
                {
                    // Add to the total pre-marker count
                    markers_total += 1;

                    // Instantiate the pre-marker
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
                }
                // If space has not been designated as empty or special
                else if (board_marker_array[i, j] == -1)
                {
                    if ((BuildBoard.GetArrayValue(i, j) > 0) && !((i == GetComponent<LevelBase>().Player_start.x) && (j == GetComponent<LevelBase>().Player_start.y)))
                    {
                        // Add to the total pre-marker count
                        markers_total += 1;

                        // Instantiate the pre-marker
                        marker_array[i, j] = GameObject.Instantiate(simple_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
                    }
                }
            }
        }

        // Update the marker text
        markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");

        // Reset the player's position and facing
        GetComponent<PlacePlayerCharacter>().ResetPlayer();

        // Reset the phase state
        PhaseController.ResetPhaseState();
    }

    public static bool LevelComplete()
    {
        if (markers_passed == markers_total)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

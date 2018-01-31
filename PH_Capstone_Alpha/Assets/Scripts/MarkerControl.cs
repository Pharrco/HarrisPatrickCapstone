using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerControl : MonoBehaviour {

    [SerializeField]
    GameObject n_active_marker, n_inactive_marker;
    static GameObject active_marker, inactive_marker;
    static int[,] board_marker_array;
    static GameObject[,] marker_array;
    [SerializeField]
    Text n_markers_text;
    static Text markers_text;
    static int markers_total, markers_passed;

    // Use this for initialization
    void Start () {
        active_marker = n_active_marker;
        inactive_marker = n_inactive_marker;
        markers_text = n_markers_text;

        board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        marker_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        board_marker_array[8, 1] = -1;
        board_marker_array[0, 0] = -1;

        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                if (board_marker_array[i, j] == 0)
                {
                    markers_total += 1;
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.4f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

                    markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
                }
            }
        }
	}
	
	public static void MarkSpace(int x, int y)
    {
        if (board_marker_array[x,y] == 0)
        {
            markers_passed += 1;
            markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");

            GameObject.Destroy(marker_array[x, y]);
            board_marker_array[x, y] = 1;
            marker_array[x,y] = GameObject.Instantiate(active_marker, new Vector3((x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(x, y) - 0.4f,( y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);
        }
    }

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

    public void ResetMarkers()
    {
        markers_total = 0;
        markers_passed = 0;

        board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        board_marker_array[8, 1] = -1;
        board_marker_array[0, 0] = -1;

        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                if (board_marker_array[i, j] == 0)
                {
                    GameObject.Destroy(marker_array[i, j]);

                    markers_total += 1;
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.4f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

                    markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
                }
            }
        }

        GetComponent<PlacePlayerCharacter>().ResetPlayer();
    }
}

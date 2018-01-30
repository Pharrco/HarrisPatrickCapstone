using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerControl : MonoBehaviour {

    [SerializeField]
    GameObject active_marker, inactive_marker;
    int[,] board_marker_array;
    GameObject[,] marker_array;
    [SerializeField]
    Text markers_text;
    int markers_total, markers_passed;

    // Use this for initialization
    void Start () {
        board_marker_array = new int[GetComponent<BuildBoard>().GetArrayHeight(), GetComponent<BuildBoard>().GetArrayWidth()];
        marker_array = new GameObject[GetComponent<BuildBoard>().GetArrayHeight(), GetComponent<BuildBoard>().GetArrayWidth()];

        board_marker_array[8, 1] = -1;
        board_marker_array[0, 0] = -1;

        for (int i = 0; i < GetComponent<BuildBoard>().GetArrayHeight(); i++)
        {
            for (int j = 0; j < GetComponent<BuildBoard>().GetArrayWidth(); j++)
            {
                if (board_marker_array[i, j] == 0)
                {
                    markers_total += 1;
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (GetComponent<BuildBoard>().GetArrayHeight() / 2)) * 5, GetComponent<BuildBoard>().GetArrayValue(i, j) - 0.4f, (j - (GetComponent<BuildBoard>().GetArrayWidth() / 2)) * 5), Quaternion.identity);

                    markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
                }
            }
        }
	}
	
	public void MarkSpace(int x, int y)
    {
        if (board_marker_array[x,y] == 0)
        {
            markers_passed += 1;
            markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");

            GameObject.Destroy(marker_array[x, y]);
            board_marker_array[x, y] = 1;
            marker_array[x,y] = GameObject.Instantiate(active_marker, new Vector3((x - (GetComponent<BuildBoard>().GetArrayHeight() / 2)) * 5, GetComponent<BuildBoard>().GetArrayValue(x, y) - 0.4f,( y - (GetComponent<BuildBoard>().GetArrayWidth() / 2)) * 5), Quaternion.identity);
        }
    }

    public bool CanMove(int x, int y)
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

        board_marker_array = new int[GetComponent<BuildBoard>().GetArrayHeight(), GetComponent<BuildBoard>().GetArrayWidth()];

        board_marker_array[8, 1] = -1;
        board_marker_array[0, 0] = -1;

        for (int i = 0; i < GetComponent<BuildBoard>().GetArrayHeight(); i++)
        {
            for (int j = 0; j < GetComponent<BuildBoard>().GetArrayWidth(); j++)
            {
                if (board_marker_array[i, j] == 0)
                {
                    GameObject.Destroy(marker_array[i, j]);

                    markers_total += 1;
                    marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (GetComponent<BuildBoard>().GetArrayHeight() / 2)) * 5, GetComponent<BuildBoard>().GetArrayValue(i, j) - 0.4f, (j - (GetComponent<BuildBoard>().GetArrayWidth() / 2)) * 5), Quaternion.identity);

                    markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
                }
            }
        }

        GetComponent<TestPlayerPlace>().ResetPlayer();
    }
}

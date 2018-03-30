using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour {

    [SerializeField]
    GameObject square_prefab;
    GameObject player_marker;
    GameObject[,] base_array, mark_array;
    [SerializeField]
    float separation;

	// Use this for initialization
	public void InitializeMiniMap () {
        base_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        mark_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each item in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                if (BuildBoard.GetArrayValue(i, j) > 0)
                {
                    base_array[i, j] = GameObject.Instantiate(square_prefab, Vector3.zero, Quaternion.identity, transform);
                    base_array[i, j].transform.localPosition = new Vector3(separation * (i - (BuildBoard.GetArrayHeight() / 2f) + 0.5f), separation * (j - (BuildBoard.GetArrayWidth() / 2f) + 0.5f), 0);
                    base_array[i, j].transform.localRotation = Quaternion.identity;
                }
            }
        }

        // For each column
        for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
        {
            // For each item in the column
            for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
            {
                if (BuildBoard.GetArrayValue(i, j) > 0)
                {
                    mark_array[i, j] = GameObject.Instantiate(square_prefab, Vector3.zero, Quaternion.identity, transform);
                    mark_array[i, j].transform.localPosition = new Vector3(separation * (i - (BuildBoard.GetArrayHeight() / 2f) + 0.5f), separation * (j - (BuildBoard.GetArrayWidth() / 2f) + 0.5f), 0);
                    mark_array[i, j].transform.localRotation = Quaternion.identity;
                    mark_array[i, j].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    mark_array[i, j].GetComponent<Image>().color = Color.clear;
                }
            }
        }

        player_marker = GameObject.Instantiate(square_prefab, Vector3.zero, Quaternion.identity, transform);
        player_marker.GetComponent<Image>().color = Color.red;
        player_marker.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }
	
	// Set the player position
    public void SetPlayerPosition(int x, int y)
    {
        player_marker.transform.localPosition = new Vector3(separation * (x - (BuildBoard.GetArrayHeight() / 2f) + 0.5f), separation * (y - (BuildBoard.GetArrayWidth() / 2f) + 0.5f), 0);
        player_marker.transform.localRotation = Quaternion.identity;
    }

    // Set the value of a given marker
    public void SetMapMarker(int new_x, int new_y, Color new_color)
    {
        mark_array[new_x, new_y].GetComponent<Image>().color = new_color;
    }
}

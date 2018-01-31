using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayerCharacter : MonoBehaviour {

    [SerializeField]
    GameObject prefab_player;
    GameObject curr_player;
    [SerializeField]
    int player_start_coord_x = 0;
    [SerializeField]
    int player_start_coord_y = 0;
    [SerializeField]
    int player_facing = 0;

    // Use this for initialization
    void Start () {
        // Create the player
        curr_player = GameObject.Instantiate(prefab_player, new Vector3((player_start_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_start_coord_x, player_start_coord_y) - 1f, (player_start_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

        // Set the camera to focus on the player character
        ControlView.SetFocus(curr_player);
    }

    public void ResetPlayer()
    {
        // Destroy the existing player character
        GameObject.Destroy(curr_player);

        // Instantiate a new player character
        curr_player = GameObject.Instantiate(prefab_player, new Vector3((player_start_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_start_coord_x, player_start_coord_y) - 1f, (player_start_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

        // Set the camera focus on the new player
        ControlView.SetFocus(curr_player);
    }
}

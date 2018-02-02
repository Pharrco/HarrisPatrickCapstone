using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayerCharacter : MonoBehaviour {

    [SerializeField]
    GameObject prefab_player;
    GameObject curr_player;
    int player_start_coord_x;
    int player_start_coord_y;
    [SerializeField]
    int player_facing = 0;

    // Use this for initialization
    void Start () {
        // Set the player's initial coordinates based on level data
        player_start_coord_x = (int)GetComponent<LevelBase>().Player_start.x;
        player_start_coord_y = (int)GetComponent<LevelBase>().Player_start.y;

        // Create the player
        curr_player = GameObject.Instantiate(prefab_player, new Vector3((player_start_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_start_coord_x, player_start_coord_y) - 1f, (player_start_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(0, (int)GetComponent<LevelBase>().Facing_start, 0));

        // Set the player character's initial data
        curr_player.GetComponent<PlayerController>().SetPosition((int)GetComponent<LevelBase>().Player_start.x, (int)GetComponent<LevelBase>().Player_start.y, (int)GetComponent<LevelBase>().Facing_start);

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

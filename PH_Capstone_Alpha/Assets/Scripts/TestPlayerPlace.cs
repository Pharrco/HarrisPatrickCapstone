using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerPlace : MonoBehaviour {

    [SerializeField]
    GameObject mock_player;
    GameObject curr_player;
    int player_coord_x = 0;
    int player_coord_y = 0;
    int player_facing = 0;

    // Use this for initialization
    void Start () {

        curr_player = GameObject.Instantiate(mock_player, new Vector3((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f, (player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);
        ControlView.SetFocus(curr_player);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhaseController.GetCurrPhase() == PhaseController.GamePhase.PlayerTurn)
        {

            bool changed = false;

            int try_x = 0;
            int try_y = 0;
            int try_turn = 0;
            bool control_input = false;

            if (Input.GetKeyDown(KeyCode.W))
            {
                control_input = true;
                try_x = player_coord_x + Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_y = player_coord_y + Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_turn = 0;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                control_input = true;
                try_x = player_coord_x - Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_y = player_coord_y - Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_turn = 180;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                control_input = true;
                try_x = player_coord_x - Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_y = player_coord_y + Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_turn = 270;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                control_input = true;
                try_x = player_coord_x + Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_y = player_coord_y - Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);
                try_turn = 90;
            }
            if (control_input)
            {
                if ((try_x >= 0) && (try_x < BuildBoard.GetArrayHeight()) && (try_y >= 0) && (try_y < BuildBoard.GetArrayWidth()))
                {
                    if ((Mathf.Abs(BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - BuildBoard.GetArrayValue(try_x, try_y)) < 1.5) && (BuildBoard.GetArrayValue(try_x, try_y) != 0))
                    {
                        if (MarkerControl.CanMove(try_x, try_y))
                        {
                            player_coord_x = try_x;
                            player_coord_y = try_y;
                            changed = true;

                            player_facing = (player_facing + try_turn) % 360;
                            PhaseController.EndPlayerTurn();
                        }
                        else
                        {
                            Debug.Log("Target (" + try_x + ", " + try_y + ") Already Marked");
                            // Damage self
                        }
                    }
                    else
                    {
                        Debug.Log("Target (" + try_x + ", " + try_y + ") Height Difference Too Large");
                    }
                }
                else
                {
                    Debug.Log("Target (" + try_x + ", " + try_y + ") Out Of Range");
                }
            }

            if (changed)
            {
                GameObject.Destroy(curr_player);

                curr_player = GameObject.Instantiate(mock_player, new Vector3((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f, (player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(0, player_facing, 0));

                MarkerControl.MarkSpace(player_coord_x, player_coord_y);

                ControlView.SetFocus(curr_player);
            }
        }
    }

    public void ResetPlayer()
    {
        GameObject.Destroy(curr_player);

        player_coord_x = 0;
        player_coord_y = 0;
        player_facing = 0;

        curr_player = GameObject.Instantiate(mock_player, new Vector3((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f, (player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);
        ControlView.SetFocus(curr_player);
    }
}

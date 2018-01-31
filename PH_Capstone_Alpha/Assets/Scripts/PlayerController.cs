using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    int player_coord_x = 0;
    int player_coord_y = 0;
    int player_facing = 0;
    int player_curr_height = 1;
    int player_target_height = 0;
    int player_target_x = 0;
    int player_target_y = 0;
    float temp_target_x;
    float temp_target_y;
    float temp_target_height;
    float temp_origin_x;
    float temp_origin_y;
    float temp_origin_height;
    float progress = 0;
    [SerializeField]
    float move_speed, error_dist;
    int move_phase = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // If the current game phase is the player's turn
        if (PhaseController.GetCurrPhase() == PhaseController.GamePhase.PlayerTurn)
        {
            // Start by assuming their will be no move
            bool control_input = false;

            // Set the try and target values to 0
            int try_x = 0;
            int try_y = 0;
            int try_turn = 0;

            // W Key, move forward
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Input received
                control_input = true;

                // Attempted x move is + Sin( facing )
                try_x = player_coord_x + Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);

                // Attempted y move is + Cos( facing )
                try_y = player_coord_y + Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);

                // No added rotation
                try_turn = 0;
            }
            // S Key, move backward
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // Input received
                control_input = true;

                // Attempted x move is - Sin( facing )
                try_x = player_coord_x - Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);

                // Attempted y move is - Cos( facing )
                try_y = player_coord_y - Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);

                // 180-degree rotation
                try_turn = 180;
            }
            // A Key, move left
            else if (Input.GetKeyDown(KeyCode.A))
            {
                // Input received
                control_input = true;

                // Attempted x move is - Cos( facing )
                try_x = player_coord_x - Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);

                // Attempted y move is + Sin( facing )
                try_y = player_coord_y + Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);

                // 270-degree rotation
                try_turn = 270;
            }
            // D Key, move right
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Input received
                control_input = true;

                // Attempted x move is + Cos( facing )
                try_x = player_coord_x + Mathf.FloorToInt(Mathf.Cos(player_facing * Mathf.Deg2Rad) + 0.5f);

                // Attempted y move is - Sin( facing )
                try_y = player_coord_y - Mathf.FloorToInt(Mathf.Sin(player_facing * Mathf.Deg2Rad) + 0.5f);

                // 90-degree rotation
                try_turn = 90;
            }

            // If input received
            if (control_input)
            {
                // If targeted space is within the limits of the board
                if ((try_x >= 0) && (try_x < BuildBoard.GetArrayHeight()) && (try_y >= 0) && (try_y < BuildBoard.GetArrayWidth()))
                {
                    // If the difference between the current space and target space is one or zero
                    if ((Mathf.Abs(BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - BuildBoard.GetArrayValue(try_x, try_y)) < 1.5) && (BuildBoard.GetArrayValue(try_x, try_y) != 0))
                    {
                        // If the space has not already been marked
                        if (MarkerControl.CanMove(try_x, try_y))
                        {
                            // Set the target space
                            player_target_x = try_x;
                            player_target_y = try_y;

                            // Set the first temp target
                            temp_target_x = ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5) + ( 1f / 3f) * (((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5) - ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5));
                            temp_target_y = ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5) + (1f / 3f) * (((player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5) - ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5));
                            temp_target_height = BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f;

                            // Set the temp origin
                            temp_origin_x = transform.position.x;
                            temp_origin_y = transform.position.z;
                            temp_origin_height = transform.position.y;

                            // Update the player's facing
                            player_facing = (player_facing + try_turn) % 360;

                            // Rotate the player character
                            transform.rotation = Quaternion.Euler( 0, player_facing, 0);

                            // Set the current and destination heights
                            player_curr_height = BuildBoard.GetArrayValue(player_coord_x, player_coord_y);
                            player_target_height = BuildBoard.GetArrayValue(try_x, try_y);

                            // Start run animation
                            GetComponent<Animator>().SetBool("Run", true);

                            // Set move phase
                            move_phase = 0;

                            // End the player input phase, begin the player animation phase
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
        }
        // Animate the player character's movement between spaces
        else if (PhaseController.GetCurrPhase() == PhaseController.GamePhase.PlayerAnimation)
        {
            // Update progress
            progress += move_speed * Time.deltaTime;

            // Lerp to temporary destination
            transform.position = Vector3.Lerp(new Vector3(temp_origin_x, temp_origin_height, temp_origin_y), new Vector3(temp_target_x, temp_target_height, temp_target_y), Mathf.Min(1f, progress));

            // If near temporary destination
            if ((move_phase < 2) && (Vector3.Distance(transform.position, new Vector3(temp_target_x, temp_target_height, temp_target_y)) < error_dist))
            {
                // If approaching first temporary destination
                if (move_phase == 0)
                {
                    // Set the temp origin
                    temp_origin_x = transform.position.x;
                    temp_origin_y = transform.position.z;
                    temp_origin_height = transform.position.y;

                    // Set the second temp target
                    temp_target_x = ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5) + (2f / 3f) * (((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5) - ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5));
                    temp_target_y = ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5) + (2f / 3f) * (((player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5) - ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5));
                    temp_target_height = BuildBoard.GetArrayValue(player_target_x, player_target_y) - 1f;
                    move_phase = 1;

                    // Reset progress
                    progress = 0;
                }
                else
                {
                    // Set the temp origin
                    temp_origin_x = transform.position.x;
                    temp_origin_y = transform.position.z;
                    temp_origin_height = transform.position.y;

                    // Set the final target
                    temp_target_x = (player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5;
                    temp_target_y = (player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5;
                    temp_target_height = BuildBoard.GetArrayValue(player_target_x, player_target_y) - 1f;
                    move_phase = 2;

                    // Reset progress
                    progress = 0;
                }
            }

            // If near final destination
            if (Vector3.Distance(transform.position, new Vector3((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_target_x, player_target_y) - 1f, (player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5)) < error_dist)
            {
                Debug.Log("Jump to destination");

                // Move to destination
                transform.position = new Vector3((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_target_x, player_target_y) - 1f, (player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5);

                // Reset progress
                progress = 0;

                // Mark the new space
                MarkerControl.MarkSpace(player_target_x, player_target_y);

                // Set the player's new position
                player_coord_x = player_target_x;
                player_coord_y = player_target_y;

                // End run animation
                GetComponent<Animator>().SetBool("Run", false);

                // Advance to next player turn
                PhaseController.EndPlayerAnimation();
            }
        }
    }
}

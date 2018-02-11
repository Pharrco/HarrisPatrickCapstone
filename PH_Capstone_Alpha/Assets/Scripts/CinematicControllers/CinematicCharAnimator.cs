using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCharAnimator : MonoBehaviour {

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
    bool active = false;

    public bool Move_complete { get; protected set; }

    // Use this for initialization
    void Start () {
        // Move is not complete
        Move_complete = false;
        transform.position = new Vector3((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f, (player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
        StartMove(0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if ((active) && (!Move_complete))
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
                Debug.Log("Finish");

                // Move to destination
                transform.position = new Vector3((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(player_target_x, player_target_y) - 1f, (player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5);

                // Reset progress
                progress = 0;

                // Set the player's new position
                player_coord_x = player_target_x;
                player_coord_y = player_target_y;

                // Set movement complete
                Move_complete = true;
            }
        }
    }

    public void StartMove(int try_x, int try_y)
    {
        // Set the target space
        player_target_x = try_x;
        player_target_y = try_y;

        // Set the first temp target
        temp_target_x = ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5) + (1f / 3f) * (((player_target_x - (BuildBoard.GetArrayHeight() / 2)) * 5) - ((player_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5));
        temp_target_y = ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5) + (1f / 3f) * (((player_target_y - (BuildBoard.GetArrayWidth() / 2)) * 5) - ((player_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5));
        temp_target_height = BuildBoard.GetArrayValue(player_coord_x, player_coord_y) - 1f;

        // Set the temp origin
        temp_origin_x = transform.position.x;
        temp_origin_y = transform.position.z;
        temp_origin_height = transform.position.y;

        // Update the player's facing
        player_facing = Mathf.FloorToInt(Mathf.Atan2((float)(try_x - player_coord_x), (float)(try_y - player_coord_y)) * Mathf.Rad2Deg);

        // Rotate the player character
        transform.rotation = Quaternion.Euler(0, player_facing, 0);

        // Set the current and destination heights
        player_curr_height = BuildBoard.GetArrayValue(player_coord_x, player_coord_y);
        player_target_height = BuildBoard.GetArrayValue(try_x, try_y);

        // Start run animation
        GetComponent<Animator>().SetBool("Run", true);

        // Set move phase
        move_phase = 0;

        active = true;
        Move_complete = false;
    }
}

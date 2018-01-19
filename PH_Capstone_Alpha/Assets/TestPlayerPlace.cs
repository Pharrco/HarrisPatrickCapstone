using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerPlace : MonoBehaviour {

    [SerializeField]
    GameObject mock_player;
    GameObject curr_player;
    int player_coord_x = 0;
    int player_coord_y = 0;

    // Use this for initialization
    void Start () {

        curr_player = GameObject.Instantiate(mock_player, new Vector3(player_coord_x - (GetComponent<BuildBoard>().GetArrayHeight() / 2), GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y), player_coord_y - (GetComponent<BuildBoard>().GetArrayWidth() / 2)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        bool changed = false;

        if (Input.GetKeyDown(KeyCode.W) && (player_coord_y < GetComponent<BuildBoard>().GetArrayWidth() - 1))
        {
            if ((Mathf.Abs(GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y) - GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y + 1)) < 1.5) && (GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y + 1) != 0))
            {
                if (GetComponent<MarkerControl>().CanMove(player_coord_x, player_coord_y + 1))
                {
                    player_coord_y += 1;
                    changed = true;
                }
                else
                {
                    // Damage self
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && (player_coord_y > 0))
        {
            if ((Mathf.Abs(GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y) - GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y - 1)) < 1.5) && (GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y - 1) != 0))
            {
                if (GetComponent<MarkerControl>().CanMove(player_coord_x, player_coord_y - 1))
                {
                    player_coord_y -= 1;
                    changed = true;
                }
                else
                {
                    // Damage self
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) && (player_coord_x > 0))
        {
            if ((Mathf.Abs(GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y) - GetComponent<BuildBoard>().GetArrayValue(player_coord_x - 1, player_coord_y)) < 1.5) && (GetComponent<BuildBoard>().GetArrayValue(player_coord_x - 1, player_coord_y) != 0))
            {
                if (GetComponent<MarkerControl>().CanMove(player_coord_x - 1, player_coord_y))
                {
                    player_coord_x -= 1;
                    changed = true;
                }
                else
                {
                    // Damage self
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && (player_coord_x < GetComponent<BuildBoard>().GetArrayHeight() - 1))
        {
            if ((Mathf.Abs(GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y) - GetComponent<BuildBoard>().GetArrayValue(player_coord_x + 1, player_coord_y)) < 1.5) && (GetComponent<BuildBoard>().GetArrayValue(player_coord_x + 1, player_coord_y) != 0))
            {
                if (GetComponent<MarkerControl>().CanMove(player_coord_x + 1, player_coord_y))
                {
                    player_coord_x += 1;
                    changed = true;
                }
                else
                {
                    // Damage self
                }
            }
        }

        if (changed)
        {
            GameObject.Destroy(curr_player);

            curr_player = GameObject.Instantiate(mock_player, new Vector3(player_coord_x - (GetComponent<BuildBoard>().GetArrayHeight() / 2), GetComponent<BuildBoard>().GetArrayValue(player_coord_x, player_coord_y), player_coord_y - (GetComponent<BuildBoard>().GetArrayWidth() / 2)), Quaternion.identity);

            GetComponent<MarkerControl>().MarkSpace(player_coord_x, player_coord_y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelectCameraControl : MonoBehaviour {

    //public static int levels_complete = 12;
    [SerializeField]
    int level_first, level_last;
    [SerializeField]
    List<Transform> camera_anchors;
    float move_progress = 0;
    int curr_position = 0;
    int dest_position = 0;
    bool moving = false;
    [SerializeField]
    GameObject select_canvas, data_canvas;
    [SerializeField]
    List<string> scene_refs;

    // Use this for initialization
    void Start () {
        // Move to first camera anchor
        transform.position = camera_anchors[0].position;
        transform.rotation = camera_anchors[0].rotation;

        GameObject.Find("LevelTitleText").GetComponent<Text>().text = "Level 1-" + (curr_position + 1).ToString();

		GameObject.Find("HUD").transform.Find("PlayerCashText").GetComponent<Text>().text = "$" + GameSave.loaded_save.GetPlayerCash().ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
        // If the camera is moving
        if (moving)
        {
            // Update movement progress
            move_progress = Mathf.Min(1.0f, move_progress + Time.deltaTime);

            // Update position
            transform.position = Vector3.Lerp(camera_anchors[curr_position].position, camera_anchors[dest_position].position, move_progress);

            // If movement is complete
            if (move_progress >= 1.0f)
            {
                // The camera is no longer moving
                moving = false;

                // Update current to destination
                curr_position = dest_position;

                // Ensure the camera is in the appropriate final position
                transform.position = camera_anchors[curr_position].position;
                transform.rotation = camera_anchors[curr_position].rotation;

                // Show the UI
                ShowUserInterface();

                // Reset move progress
                move_progress = 0;
            }
        }
        else
        {
            // Get keyboard input left
            if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                MoveSceneSelLeft();
            }
            // Get keyboard input right
            else if ((Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.RightArrow)))
            {
                MoveSceneSelRight();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                StartLevel();
            }
            else if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.Backspace)))
            {
                ReturnToMenu();
            }
        }
	}

    // Move left
    public void MoveSceneSelLeft()
    {
        // If this is not the first level
        if (curr_position > 0)
        {
            // The camera can now move
            moving = true;

            // Set destination one to the right
            dest_position = curr_position - 1;

            // Hide the UI
            HideUserInterface();
        }
    }

    // Move right
    public void MoveSceneSelRight()
    {
        // If the next level is unlocked and this is not the final level
        if ((curr_position <= (GameSave.loaded_save.GetLevelProgress() - level_first)) && (curr_position <= (level_last - level_first)))
        {
            // The camera can now move
            moving = true;

            // Set destination one to the right
            dest_position = curr_position + 1;

            // Hide the UI
            HideUserInterface();
        }
    }

    // Hide the UI
    void HideUserInterface()
    {
        select_canvas.SetActive(false);
        data_canvas.SetActive(false);
    }

    // Show the UI
    void ShowUserInterface()
    {
        select_canvas.SetActive(true);
        data_canvas.SetActive(true);

        GameObject.Find("LevelTitleText").GetComponent<Text>().text = "Level 1-" + (curr_position + 1).ToString();
    }

    // Open the level currently selected
    public void StartLevel()
    {
        SceneManager.LoadScene(scene_refs[curr_position]);
    }

    // Return to the main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu_Main");
    }
}

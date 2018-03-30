using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlView : MonoBehaviour {

    private enum ViewCameraMode { Free, Lock, Follow }

    static GameObject camera_main;
    static ViewCameraMode cam_mode = ViewCameraMode.Follow;
    public static float rotation = 90;
    public static float slope = 0;
    public static float zoom = 0;
    static float rotate_timer;
    static float curr_target;
    static GameObject current_focus;
    [SerializeField]
    float rotation_speed;
    float lock_offset = 180;
    static PlayerController player_ref;
    [SerializeField]
    GameObject WallN, WallE, WallS, WallW;
    [SerializeField]
    Sprite sprite_free, sprite_follow, sprite_lock;
    [SerializeField]
    GameObject HUD_Camera;

    // Use this for initialization
    void Start () {

        // Get the main camera
        camera_main = Camera.main.gameObject;

        player_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        cam_mode = ViewCameraMode.Follow;

        HUD_Camera.transform.GetChild(0).GetComponent<Image>().sprite = sprite_follow;

		float shift_width = ((BuildBoard.GetArrayWidth() + 2) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_height = ((BuildBoard.GetArrayHeight() + 2) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_center = Constants.TILE_SEPARATION / 2.0f;

		WallN.transform.position = new Vector3(shift_height - shift_center, -2f, -shift_center);
		WallS.transform.position = new Vector3((-shift_height) - shift_center, -2f, -shift_center);
		WallE.transform.position = new Vector3( -shift_center, -2f, (-shift_width) - shift_center);
		WallW.transform.position = new Vector3(-shift_center, -2f, shift_width - shift_center);
	}
	
	// Update is called once per frame
	void Update () {

        // If the camera is in "Follow" mode
        if (cam_mode == ViewCameraMode.Follow)
        {
            // If the player has started to move the camera's rotation
            if (Mathf.Abs(Input.GetAxisRaw("ViewHorizontal")) > 0)
            {
                cam_mode = ViewCameraMode.Free;
                HUD_Camera.transform.GetChild(0).GetComponent<Image>().sprite = sprite_free;
            }
        }
        else if (cam_mode == ViewCameraMode.Lock) // If the camera is in "Lock" mode
        {
            // If the player has started to move the camera's rotation
            if (Mathf.Abs(Input.GetAxisRaw("ViewHorizontal")) > 0)
            {
                cam_mode = ViewCameraMode.Free;
                HUD_Camera.transform.GetChild(0).GetComponent<Image>().sprite = sprite_free;
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                cam_mode = ViewCameraMode.Follow;
                lock_offset = 180;
                HUD_Camera.transform.GetChild(0).GetComponent<Image>().sprite = sprite_follow;
            }
        }
        else if (cam_mode == ViewCameraMode.Free)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                cam_mode = ViewCameraMode.Lock;
                lock_offset = rotation - (player_ref.GetFacing());
                HUD_Camera.transform.GetChild(0).GetComponent<Image>().sprite = sprite_lock;
            }
        }

        if (cam_mode == ViewCameraMode.Free)
        {
            // Update rotation based on player input
            rotation -= Input.GetAxisRaw("ViewHorizontal");
        }
        else
        {

            rotate_timer += Time.deltaTime;
            rotate_timer = Mathf.Min(rotation_speed, rotate_timer);

            rotation = Mathf.LerpAngle(rotation, (player_ref.GetFacing()) + lock_offset, rotate_timer / rotation_speed);
        }

        if ((rotate_timer >= rotation_speed) && (Mathf.Abs((player_ref.GetFacing()) + lock_offset - rotation) > 89))
        {
            rotate_timer = 0;
        }

        // Update slope based on player input
        slope = Mathf.Max(0, Mathf.Min(6, slope + (Input.GetAxisRaw("ViewVertical") / 10)));

        // Update zoom based on user input
        zoom = Mathf.Max(0, Mathf.Min(3, zoom + (Input.GetAxisRaw("Zoom") / 6)));

        // Fix rotation within 360
        rotation = ((int)rotation + 360) % 360;

        // If the current focus target is set
        if ((current_focus != null) && (current_focus.transform != null))
        {
            // Position the camera
            camera_main.transform.position = new Vector3(current_focus.transform.position.x + (10 - slope - zoom) * Mathf.Sin(rotation * Mathf.Deg2Rad), current_focus.transform.position.y + 6 + slope - zoom, current_focus.transform.position.z + (10 - slope - zoom) * Mathf.Cos(rotation * Mathf.Deg2Rad));

            // Look at the focus target
            camera_main.transform.LookAt(current_focus.transform.position);
        }


        // Set South wall based on visibility
        if ((rotation < 320) && (rotation > 210))
        {
            WallS.SetActive(false);
        }
        else
        {
            WallS.SetActive(true);
        }

        // Set North wall based on visibility
        if ((rotation < 140) && (rotation > 30))
        {
            WallN.SetActive(false);
        }
        else
        {
            WallN.SetActive(true);
        }

        // Set East wall based on visibility
        if ((rotation < 230) && (rotation > 120))
        {
            WallE.SetActive(false);
        }
        else
        {
            WallE.SetActive(true);
        }

        // Set East wall based on visibility
        if ((rotation < 50) || (rotation > 300))
        {
            WallW.SetActive(false);
        }
        else
        {
            WallW.SetActive(true);
        }
    }

    // Set the focus to the provided target
    public static void SetFocus(GameObject new_target)
    {
        // Set the focus to the provided target
        current_focus = new_target;

        player_ref = new_target.GetComponent<PlayerController>();

        // If focus target and camera exist
        if ((current_focus.transform != null) && (camera_main != null))
        {
            // Look at the focus target
            camera_main.transform.LookAt(current_focus.transform.position);
        }

    }

    // Adds a total of amount of rotation to force the camera to do, allows matching of camera to player rotation for more control clarity
    // Calling this replaces the current value, so it will cancel any other forced camera rotation already in progress
    //public static void AddForcedRotation(float n_rotation)
    //{
    //    // Checks to see if the feature is enabled
    //    if (force_to_target)
    //    {
    //        // Set the origin and destination of the rotation
    //        temp_origin = rotation;
    //        temp_target = (int)(rotation + n_rotation) % 360;

    //        // Set that a target is present
    //        has_target = true;

    //        // Reset progress
    //        rotation_status = 0;
    //    }
    //}
}

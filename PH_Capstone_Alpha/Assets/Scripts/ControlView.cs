using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlView : MonoBehaviour {

    static GameObject camera_main;
    public static float rotation = 225;
    public static float slope = 0;
    public static float zoom = 0;
    static GameObject current_focus;
    static bool force_to_target = true;
    static bool has_target = false;
    static float temp_origin, temp_target;
    static float rotation_status = 0;
    [SerializeField]
    GameObject WallN, WallE, WallS, WallW;

    // Use this for initialization
    void Start () {

        // Get the main camera
        camera_main = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        // Force the camera to roate to a target angle if feature is turned on and a target exists
        if ((force_to_target) && (has_target))
        {
            // Update time
            rotation_status = Mathf.Min(1.0f, rotation_status + Time.deltaTime);

            // Update rotation
            rotation = Mathf.LerpAngle(temp_origin, temp_target, rotation_status);

            // If rotation is complete
            if (rotation_status >= 1.0f)
            {
                // No longer has a target
                has_target = false;

                // Reset rotation status
                rotation_status = 0;
            }

            // If player has started to adjust angle on their own
            if (Mathf.Abs(Input.GetAxisRaw("ViewHorizontal")) > 0)
            {
                // Cancel target
                has_target = false;

                // Reset rotation status
                rotation_status = 0;
            }
        }

        // Update rotation based on player input
        rotation -= Input.GetAxisRaw("ViewHorizontal");

        // Fix rotation within 360
        rotation = ((int)rotation + 360) % 360;

        // Update slope based on player input
        slope = Mathf.Max(0, Mathf.Min(6, slope + (Input.GetAxisRaw("ViewVertical") / 10)));

        // Update zoom based on user input
        zoom = Mathf.Max(0, Mathf.Min(3, zoom + (Input.GetAxisRaw("Zoom") / 6)));

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

        // If focus target and camera exist
        if ((current_focus.transform != null) && (camera_main != null))
        {
            // Look at the focus target
            camera_main.transform.LookAt(current_focus.transform.position);
        }

    }

    // Adds a total of amount of rotation to force the camera to do, allows matching of camera to player rotation for more control clarity
    // Calling this replaces the current value, so it will cancel any other forced camera rotation already in progress
    public static void AddForcedRotation(float n_rotation)
    {
        // Checks to see if the feature is enabled
        if (force_to_target)
        {
            // Set the origin and destination of the rotation
            temp_origin = rotation;
            temp_target = (int)(rotation + n_rotation) % 360;

            // Set that a target is present
            has_target = true;

            // Reset progress
            rotation_status = 0;
        }
    }
}

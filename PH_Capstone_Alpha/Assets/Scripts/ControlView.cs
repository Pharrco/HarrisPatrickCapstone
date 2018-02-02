using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlView : MonoBehaviour {

    static GameObject camera_main;
    float rotation = 225;
    public float slope = 0;
    public float zoom = 0;
    static GameObject current_focus;
    static bool force_turn = true;
    [SerializeField]
    float force_speed;
    static float force_total;

    // Use this for initialization
    void Start () {

        // Get the main camera
        camera_main = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        // Force the camera to rotate if feature is turned on and some forced rotation remains
        if ((force_turn) && (force_total > 0))
        {
            // Add the remianing rotation or max forced rotations speed, whichever is less, to the rotation
            rotation += Mathf.Min(force_total, force_speed);

            // Subtract the remianing rotation or max forced rotations speed, whichever is less, from the remaining rotation
            force_total -= Mathf.Min(force_total, force_speed);
        }
        else if ((force_turn) && (force_total < 0)) // Negative protection
        {
            // Subtract the remianing rotation or max forced rotations speed, whichever is less, from the rotation
            rotation += Mathf.Max(force_total, -force_speed);

            // Add the remianing rotation or max forced rotations speed, whichever is less, to the remaining rotation
            force_total -= Mathf.Max(force_total, -force_speed);
        }

        // Update rotation based on player input
        rotation -= Input.GetAxisRaw("ViewHorizontal");

        // Fix rotation within 360
        rotation = (int)rotation % 360;

        // Update slope based on player input
        slope = Mathf.Max(0, Mathf.Min(6, slope + (Input.GetAxisRaw("ViewVertical") / 10)));

        // Update zoom based on user input
        zoom = Mathf.Max(0, Mathf.Min(3, zoom + (Input.GetAxisRaw("Zoom") / 6)));

        // If the current focus target is set
        if (current_focus.transform != null)
        {
            // Position the camera
            camera_main.transform.position = new Vector3(current_focus.transform.position.x + (10 - slope - zoom) * Mathf.Sin(rotation * Mathf.Deg2Rad), current_focus.transform.position.y + 6 + slope - zoom, current_focus.transform.position.z + (10 - slope - zoom) * Mathf.Cos(rotation * Mathf.Deg2Rad));

            // Look at the focus target
            camera_main.transform.LookAt(current_focus.transform.position);
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
        if (force_turn)
        {
            // Change the total amount of rotation to the new value
            force_total = n_rotation;
        }
    }
}

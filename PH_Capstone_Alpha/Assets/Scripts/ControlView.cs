using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlView : MonoBehaviour {

    static GameObject camera_main;
    int rotation = 225;
    public float slope = 0;
    public float zoom = 0;
    static GameObject current_focus;

    // Use this for initialization
    void Start () {
        // Get the main camera
        camera_main = Camera.main.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

        // Update rotation based on player input
        rotation -= (int)Input.GetAxisRaw("ViewHorizontal");

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

        if ((current_focus.transform != null) && (camera_main != null))
        {
            // Look at the focus target
            camera_main.transform.LookAt(current_focus.transform.position);
        }

    }
}

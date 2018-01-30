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

        camera_main = Camera.main.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

        rotation -= (int)Input.GetAxisRaw("ViewHorizontal");

        rotation = (int)rotation % 360;

        slope = Mathf.Max(0, Mathf.Min(6, slope + (Input.GetAxisRaw("ViewVertical") / 10)));

        zoom = Mathf.Max(0, Mathf.Min(3, zoom + (Input.GetAxisRaw("Zoom") / 6)));

        if (current_focus != null)
        {
            camera_main.transform.position = new Vector3(current_focus.transform.position.x + (10 - slope - zoom) * Mathf.Sin(rotation * Mathf.Deg2Rad), current_focus.transform.position.y + 6 + slope - zoom, current_focus.transform.position.z + (10 - slope - zoom) * Mathf.Cos(rotation * Mathf.Deg2Rad));

            camera_main.transform.LookAt(current_focus.transform.position);
        }
    }

    public static void SetFocus(GameObject new_target)
    {
        current_focus = new_target;
        camera_main.transform.LookAt(current_focus.transform.position);
    }
}

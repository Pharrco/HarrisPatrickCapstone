using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlView : MonoBehaviour {

    GameObject camera_main;
    int rotation = 225;
    public float slope = 0;
    public float zoom = 0;

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

        camera_main.transform.position = new Vector3((14 - slope - zoom) * Mathf.Sin(rotation * Mathf.Deg2Rad), 10 + slope - zoom, (14 - slope - zoom) * Mathf.Cos(rotation * Mathf.Deg2Rad));

        camera_main.transform.LookAt(new Vector3(0, 0, 0));

    }
}

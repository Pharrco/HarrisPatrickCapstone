using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {

	int rotation;
	[SerializeField]
	int deg_p_frame;

	// Use this for initialization
	void Start () {
		rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rotation += deg_p_frame;
		rotation = (rotation + 360) % 360;

		transform.rotation = Quaternion.Euler(0, rotation, 0);
	}
}

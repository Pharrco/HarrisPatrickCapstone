using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuideViewControl : MonoBehaviour {

	[SerializeField]
	Transform target_transform;
	int rotation;

	// Use this for initialization
	void Start () {

		rotation = 180;
		target_transform.rotation = Quaternion.Euler( 0, rotation, 0);
	}
	
	public void RotateRight()
	{
		rotation = (rotation + 345) % 360;
		target_transform.rotation = Quaternion.Euler( 0, rotation, 0);
	}

	public void RotateLeft()
	{
		rotation = (rotation + 375) % 360;
		target_transform.rotation = Quaternion.Euler( 0, rotation, 0);
	}
}

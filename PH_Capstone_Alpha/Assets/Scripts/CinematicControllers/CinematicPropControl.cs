using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPropControl : MonoBehaviour {

	public void MoveTo(Vector3 n_coord)
	{

		transform.position = n_coord;
	}

	public void Despawn()
	{
		transform.position = new Vector3(100, 100, 100);
	}
}

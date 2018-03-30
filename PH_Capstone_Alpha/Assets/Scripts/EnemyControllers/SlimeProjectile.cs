using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour {

	Vector3 origin, destination;
	[SerializeField]
	float move_speed, error_dist;
	float progress = 0;

	public void Initialize(Vector3 n_origin, Vector3 n_destination)
	{
		origin = n_origin;
		destination = n_destination;

		transform.position = origin;
	}

	// Update is called once per frame
	void Update()
	{
		if ((origin != null) && (destination != null))
		{
			progress += Time.deltaTime * (move_speed / Vector3.Distance(origin, destination));

			transform.position = Vector3.Lerp(origin, destination, progress);

			if (Vector3.Distance(transform.position, destination) <= error_dist)
			{
				Destroy(gameObject);
			}
		}
	}
}

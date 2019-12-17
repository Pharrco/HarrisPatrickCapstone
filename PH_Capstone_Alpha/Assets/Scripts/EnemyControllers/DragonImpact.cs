using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonImpact : MonoBehaviour {
	void Update () {
		if (!GetComponent<ParticleSystem>().isPlaying)
		{
			Destroy(gameObject);
		}
	}
}

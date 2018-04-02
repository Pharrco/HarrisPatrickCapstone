using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleton : MonoBehaviour {

    public static EnemySingleton singleton;
    [SerializeField]
    public GameObject false_chest, true_chest, simple_slime;
	[SerializeField]
	public GameObject red_switch, blue_switch, green_switch, purple_switch, red_endPoint, blue_endPoint, green_endPoint, purple_endPoint;

	// Use this for initialization
	void Awake () {

        // If the singleton does not already exist
		if (singleton == null)
        {
            // Set this instance as the singleton
            singleton = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}

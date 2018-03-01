using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

    static List<GameObject> environment_list;

    // Use this for initialization
    public static void Initialize()
    {
        environment_list = new List<GameObject>();
    }

    public static void AddEnvironmentObject(GameObject n_object)
    {
        // If the list exists
        if (environment_list != null)
        {
            // If the object being added is not null and has a valid component
            if ((n_object != null) && (n_object.GetComponent<BaseEnvironmentController>() != null))
            {
                // Add the object to the list
                environment_list.Add(n_object);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSingleton : MonoBehaviour {

    public static EnvironmentSingleton singleton;
    [SerializeField]
    public GameObject slime_spawner, dragon_blast;

    // Use this for initialization
    void Awake()
    {

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

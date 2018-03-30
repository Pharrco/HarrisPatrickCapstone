using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSingleton : MonoBehaviour {

	public static InterfaceSingleton singleton;
	[SerializeField]
	public Sprite enemy_turn_icon, player_turn_icon, ally_turn_icon, environment_turn_icon, pause_turn_icon;

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

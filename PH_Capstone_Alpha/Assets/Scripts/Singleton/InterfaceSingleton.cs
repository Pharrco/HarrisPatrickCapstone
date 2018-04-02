using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSingleton : MonoBehaviour {

	public static InterfaceSingleton singleton;
	[SerializeField]
	public Sprite enemy_turn_icon, player_turn_icon, ally_turn_icon, environment_turn_icon, pause_turn_icon, chargeAble_icon, chargeUnable_icon;
	[SerializeField]
	public Sprite timer_image1, timer_image2, timer_image3, timer_image4, timer_image5, timer_imagepass, timer_imagefail, timer_imagepause;
	[SerializeField]
	public GameObject timerFrame_prefab;

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

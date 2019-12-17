using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_DoorControl : MonoBehaviour {

	[SerializeField]
	Transform doorPoint_Right, doorPoint_Left;
	Transform doorPoint_Start, doorPoint_End;
	[SerializeField]
	GameObject slime_prefab, chest_prefab, gorgon_prefab, reaper_prefab;

	GameObject curr_char;
	float progress, move_duration, move_speed;
	float wait_timer, wait_duration;
	int rotation;
	GameObject[] prefabs;

	// Use this for initialization
	void Start () {

		prefabs = new GameObject[4] { slime_prefab, chest_prefab, gorgon_prefab, reaper_prefab };

		progress = 0;
		move_speed = 2.0f;
		move_duration = Vector3.Distance(doorPoint_Right.position, doorPoint_Left.position) / move_speed;

		wait_duration = Random.Range(0.0f, 7.0f);
		wait_timer = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (curr_char != null)
		{
			progress += Time.deltaTime;

			float move_progress;

			if (progress >= move_duration)
			{
				progress = 0;
				move_progress = 0;

				Destroy(curr_char);

				wait_duration = Random.Range(1.0f, 4.0f);
				wait_timer = 0;
			}
			else
			{
				move_progress = progress / move_duration;
			}

			curr_char.transform.position = Vector3.Lerp(doorPoint_Start.position, doorPoint_End.position, move_progress);
		}
		else
		{
			wait_timer += Time.deltaTime;

			if (wait_timer >= wait_duration)
			{
				if (Random.Range(-1.0f, 1.0f) > 0)
				{
					doorPoint_Start = doorPoint_Right;
					doorPoint_End = doorPoint_Left;
					rotation = -90;
				}
				else
				{
					doorPoint_Start = doorPoint_Left;
					doorPoint_End = doorPoint_Right;
					rotation = 90;
				}

				int rand_int = Random.Range(0, prefabs.Length);

				curr_char = GameObject.Instantiate(prefabs[rand_int], doorPoint_Start.position, Quaternion.Euler(0, rotation, 0));
			}
		}
	}
}

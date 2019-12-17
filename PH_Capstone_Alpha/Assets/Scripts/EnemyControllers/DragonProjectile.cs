using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonProjectile : MonoBehaviour {

	Vector3 origin, destination, impact;
	float move_progress;
	public Vector2 position;

	[SerializeField]
	GameObject impact_prefab;

	[SerializeField]
	float move_duration = 1f;

	// Use this for initialization
	void Start ()
	{
		move_progress = 0;

		origin = transform.position;
		destination = new Vector3(origin.x, origin.y - 3f, origin.z);
		impact = new Vector3(origin.x, origin.y - 2.7f, origin.z);
	}
	
	// Update is called once per frame
	void Update () {
		move_progress += Time.deltaTime / move_duration;

		if (move_progress < 1.0)
		{
			transform.position = Vector3.Lerp(origin, destination, move_progress);
		}
		else
		{
			if ((PlayerLocator.Player_Pos_X == position.x) && (PlayerLocator.Player_Pos_Y == position.y))
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Take Damage");
				PlayerHealth.PlayerTakeDamage();
			}

			GameObject.Instantiate(impact_prefab, impact, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalGorgon : BaseEnvironmentController
{
	int target_player_rotation = -1;
	[SerializeField]
	GameObject projectile_prefab;
	GameObject active_projectile;

	public override void GetMove()
	{
		// If the target roation has been set
		if (target_player_rotation != -1)
		{
			if (target_player_rotation == GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetFacing())
			{
				MoveComplete = false;

				active_projectile = GameObject.Instantiate(projectile_prefab);
				active_projectile.GetComponent<GorgonProjectile>().Initialize(transform.position + Vector3.up, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up);
			}
		}
	}

	private void Update()
	{
		if (PhaseController.GetCurrPhase() == GamePhase.EnvironmentResult)
		{
			if (!MoveComplete)
			{
				if (active_projectile == null)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ActivateGorgonLock();
					MoveComplete = true;
				}
			}
		}
	}

	public override void Reset() { }

	public void Initialize(Enumerators.CompassDirect n_direction)
	{
		MoveComplete = true;

		float shift_width = ((BuildBoard.GetArrayWidth() + 1) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_height = ((BuildBoard.GetArrayHeight() + 1) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_center = Constants.TILE_SEPARATION / 2.0f;

		switch (n_direction)
		{
			case Enumerators.CompassDirect.North:
				transform.position = new Vector3(shift_height - shift_center, 5f, -shift_center);
				transform.rotation = Quaternion.Euler(0f, 270f, 0f);
				target_player_rotation = 90;
				break;
			case Enumerators.CompassDirect.East:
				transform.position = new Vector3(-shift_center, 5f, (-shift_width) - shift_center);
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				target_player_rotation = 180;
				break;
			case Enumerators.CompassDirect.South:
				transform.position = new Vector3((-shift_height) - shift_center, 5f, -shift_center);
				transform.rotation = Quaternion.Euler(0f, 90f, 0f);
				target_player_rotation = 270;
				break;
			case Enumerators.CompassDirect.West:
				transform.position = new Vector3(-shift_center, 5f, shift_width - shift_center);
				transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				target_player_rotation = 0;
				break;
		}
	}
}

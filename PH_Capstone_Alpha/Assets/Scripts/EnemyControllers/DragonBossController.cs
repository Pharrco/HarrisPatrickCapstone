using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonBossController : BaseEnvironmentController
{
	List<Vector3Int> targetPoints_North, targetPoints_East, targetPoints_South, targetPoints_West;
	Vector3 portalPos_North, portalPos_East, portalPos_South, portalPos_West, dragon_spawn, dragon_destination, portal_destination, dragonPos_North, dragonPos_East, dragonPos_South, dragonPos_West;
	int moves_till_spawn, cycle_rate;
	GameObject timer_panel, dragon_object, startPortal_object, endPortal_object;
	Enumerators.CompassDirect dragon_origin;
	List<GameObject> markers;
	float move_progress, dragon_rotation;

	[SerializeField]
	GameObject inPortal_Prefab, outPortal_Prefab, dragon_prefab, marker_prefab, projectile_prefab;

	void Update()
	{
		if (PhaseController.GetCurrPhase() == GamePhase.EnvironmentResult)
		{
			if (!MoveComplete)
			{
				if (dragon_object != null)
				{
					move_progress += Time.deltaTime / 3.0f;

					if (move_progress > 1.0f)
					{
						TestDamage(dragon_origin);

						Destroy(dragon_object);

						RandomSelectDirection();

						MoveComplete = true;

						moves_till_spawn = cycle_rate;

						UpdateTimer(moves_till_spawn);

						Destroy(endPortal_object);
					}

					dragon_object.transform.position = Vector3.Lerp(dragon_spawn, dragon_destination, move_progress);
				}
			}
		}

	}

	public void Initialize(int n_cycle_rate)
	{
		timer_panel = GameObject.Instantiate(InterfaceSingleton.singleton.dragonTimerFrame_prefab, GameObject.Find("TimerPanelArea").transform);

		timer_panel.transform.Find("Switch/SwitchImage").GetComponent<Image>().color = Color.clear;
		timer_panel.transform.Find("Switch").GetComponent<Image>().color = Color.clear;
		timer_panel.transform.Find("SwitchFrame").GetComponent<Image>().color = Color.clear;

		markers = new List<GameObject>();

		cycle_rate = n_cycle_rate;
		moves_till_spawn = cycle_rate;

		move_progress = 0;

		UpdateTimer(moves_till_spawn);

		UpdateTargetPoints();
		UpdatePortalPoints();

		RandomSelectDirection();

		MoveComplete = true;
	}

	public override void GetMove()
	{
		// Decrement countdown
		moves_till_spawn--;

		UpdateTimer(moves_till_spawn);

		// If countdown at 0
		if (moves_till_spawn <= 0)
		{
			MoveComplete = false;
			move_progress = 0;
			dragon_object = GameObject.Instantiate(dragon_prefab, dragon_spawn, Quaternion.Euler(0, dragon_rotation, 0));
			endPortal_object = GameObject.Instantiate(outPortal_Prefab, portal_destination, Quaternion.Euler(0, dragon_rotation, 0));

			PlaceProjectiles(dragon_origin);
		}
	}

	public override void Reset()
	{

	}

	public override void EndTurnUpdate()
	{

	}

	void UpdateTargetPoints()
	{
		int[,] board_array = BuildBoard.GetBoardArray();
		targetPoints_North = new List<Vector3Int>();
		targetPoints_East = new List<Vector3Int>();
		targetPoints_South = new List<Vector3Int>();
		targetPoints_West = new List<Vector3Int>();

		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			int set_height = 0;

			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				set_height--;

				if (j == 0)
				{
					targetPoints_East.Add(new Vector3Int(i, board_array[i, j], j));

					set_height = board_array[i, j];
				}
				else if (board_array[i, j] >= set_height)
				{
					targetPoints_East.Add(new Vector3Int(i, board_array[i, j], j));

					set_height = board_array[i, j];
				}
			}

			for (int j = BuildBoard.GetArrayWidth() - 1; j >= 0; j--)
			{
				set_height--;

				if (j == BuildBoard.GetArrayWidth() - 1)
				{
					targetPoints_West.Add(new Vector3Int(i, board_array[i, j], j));

					set_height = board_array[i, j];
				}
				else if (board_array[i, j] >= set_height)
				{
					targetPoints_West.Add(new Vector3Int(i, board_array[i, j], j));

					set_height = board_array[i, j];
				}
			}
		}

		for (int i = 0; i < BuildBoard.GetArrayWidth(); i++)
		{
			int set_height = 0;

			for (int j = 0; j < BuildBoard.GetArrayHeight(); j++)
			{
				set_height--;

				if (j == 0)
				{
					targetPoints_South.Add(new Vector3Int(j, board_array[j, i], i));

					set_height = board_array[j, i];
				}
				else if (board_array[j, i] >= set_height)
				{
					targetPoints_South.Add(new Vector3Int(j, board_array[j, i], i));

					set_height = board_array[j, i];
				}
			}

			for (int j = BuildBoard.GetArrayHeight() - 1; j >= 0; j--)
			{
				set_height--;

				if (j == BuildBoard.GetArrayHeight() - 1)
				{
					targetPoints_North.Add(new Vector3Int(j, board_array[j, i], i));

					set_height = board_array[j, i];
				}
				else if (board_array[j, i] >= set_height)
				{
					targetPoints_North.Add(new Vector3Int(j, board_array[j, i], i));

					set_height = board_array[j, i];
				}
			}
		}
	}

	void UpdatePortalPoints()
	{
		float shift_width = ((BuildBoard.GetArrayWidth() + 1.5f) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_height = ((BuildBoard.GetArrayHeight() + 1.5f) * Constants.TILE_SEPARATION) / 2.0f;
		float d_shift_width = ((BuildBoard.GetArrayWidth() + 5.5f) * Constants.TILE_SEPARATION) / 2.0f;
		float d_shift_height = ((BuildBoard.GetArrayHeight() + 5.5f) * Constants.TILE_SEPARATION) / 2.0f;
		float shift_center = Constants.TILE_SEPARATION / 2.0f;

		portalPos_North = new Vector3(shift_height - shift_center, 10f, -shift_center);
		portalPos_East = new Vector3(-shift_center, 10f, (-shift_width) - shift_center);
		portalPos_South = new Vector3((-shift_height) - shift_center, 10f, -shift_center);
		portalPos_West = new Vector3(-shift_center, 10f, shift_width - shift_center);

		dragonPos_North = new Vector3(d_shift_height - shift_center, 4f, -shift_center);
		dragonPos_East = new Vector3(-shift_center, 4f, (-d_shift_width) - shift_center);
		dragonPos_South = new Vector3((-d_shift_height) - shift_center, 4f, -shift_center);
		dragonPos_West = new Vector3(-shift_center, 4f, d_shift_width - shift_center);
	}

	public void UpdateTimer(int turn_count)
	{
		// Based on the provided number of turns remaining
		switch (turn_count)
		{
			case 1:
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_image1;
				break;
			case 2:
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_image2;
				break;
			case 3:
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_image3;
				break;
			case 4:
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_image4;
				break;
			case 5:
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_image5;
				break;
		}
	}

	public void UpdateTimer(string special)
	{
		// Based on special instruction string
		switch (special)
		{
			case "pause":
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_imagepause;
				break;
			case "spawn":
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_imagepass;
				break;
			case "fail":
				timer_panel.transform.Find("Timer/TimerImage").GetComponent<Image>().sprite = InterfaceSingleton.singleton.timer_imagefail;
				break;
		}
	}

	void RandomSelectDirection()
	{
		ClearMarkers();

		int rand_select = Random.Range(0, 4);

		switch (rand_select)
		{
			case 0:
				Destroy(startPortal_object);
				dragon_spawn = dragonPos_North;
				dragon_destination = dragonPos_South;
				portal_destination = portalPos_South;
				dragon_origin = Enumerators.CompassDirect.North;
				dragon_rotation = 270;
				startPortal_object = GameObject.Instantiate(inPortal_Prefab, portalPos_North, Quaternion.Euler(0, dragon_rotation, 0));
				break;
			case 1:
				Destroy(startPortal_object);
				dragon_spawn = dragonPos_South;
				dragon_destination = dragonPos_North;
				portal_destination = portalPos_North;
				dragon_origin = Enumerators.CompassDirect.South;
				dragon_rotation = 90;
				startPortal_object = GameObject.Instantiate(inPortal_Prefab, portalPos_South, Quaternion.Euler(0, dragon_rotation, 0));
				break;
			case 2:
				Destroy(startPortal_object);
				dragon_spawn = dragonPos_East;
				dragon_destination = dragonPos_West;
				portal_destination = portalPos_West;
				dragon_origin = Enumerators.CompassDirect.East;
				dragon_rotation = 0;
				startPortal_object = GameObject.Instantiate(inPortal_Prefab, portalPos_East, Quaternion.Euler(0, dragon_rotation, 0));
				break;
			case 3:
				Destroy(startPortal_object);
				dragon_spawn = dragonPos_West;
				dragon_destination = dragonPos_East;
				portal_destination = portalPos_East;
				dragon_origin = Enumerators.CompassDirect.West;
				dragon_rotation = 180;
				startPortal_object = GameObject.Instantiate(inPortal_Prefab, portalPos_West, Quaternion.Euler(0, dragon_rotation, 0));
				break;
		}

		PlaceMarkers(dragon_origin);
	}

	void ClearMarkers()
	{
		foreach (GameObject marker in markers)
		{
			Destroy(marker);
		}

		markers.Clear();
	}

	void PlaceMarkers(Enumerators.CompassDirect n_direction)
	{
		List<Vector3Int> current_set;

		switch (n_direction)
		{
			case Enumerators.CompassDirect.North:
				current_set = targetPoints_North;
				break;
			case Enumerators.CompassDirect.South:
				current_set = targetPoints_South;
				break;
			case Enumerators.CompassDirect.East:
				current_set = targetPoints_East;
				break;
			case Enumerators.CompassDirect.West:
				current_set = targetPoints_West;
				break;
			default:
				current_set = targetPoints_West;
				break;
		}

		foreach (Vector3Int target in current_set)
		{
			GameObject n_object = GameObject.Instantiate(marker_prefab, new Vector3((target.x - (BuildBoard.GetArrayHeight() / 2)) * 5, target.y - 0.85f, (target.z - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90,0,0));
			markers.Add(n_object);
		}
	}

	void PlaceProjectiles(Enumerators.CompassDirect n_direction)
	{
		List<Vector3Int> current_set;

		switch (n_direction)
		{
			case Enumerators.CompassDirect.North:
				current_set = targetPoints_North;
				break;
			case Enumerators.CompassDirect.South:
				current_set = targetPoints_South;
				break;
			case Enumerators.CompassDirect.East:
				current_set = targetPoints_East;
				break;
			case Enumerators.CompassDirect.West:
				current_set = targetPoints_West;
				break;
			default:
				current_set = targetPoints_West;
				break;
		}

		foreach (Vector3Int target in current_set)
		{
			GameObject n_object = GameObject.Instantiate(projectile_prefab, new Vector3((target.x - (BuildBoard.GetArrayHeight() / 2)) * 5, target.y + 3f, (target.z - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);
			n_object.GetComponent<DragonProjectile>().position = new Vector2(target.x, target.z);
		}
	}

	void TestDamage(Enumerators.CompassDirect n_direction)
	{
		List<Vector3Int> current_set;

		switch (n_direction)
		{
			case Enumerators.CompassDirect.North:
				current_set = targetPoints_North;
				break;
			case Enumerators.CompassDirect.South:
				current_set = targetPoints_South;
				break;
			case Enumerators.CompassDirect.East:
				current_set = targetPoints_East;
				break;
			case Enumerators.CompassDirect.West:
				current_set = targetPoints_West;
				break;
			default:
				current_set = targetPoints_West;
				break;
		}

		foreach (Vector3Int target in current_set)
		{
			
		}
	}
}

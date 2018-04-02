using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeSpawner : BaseEnvironmentController
{
	int moves_till_spawn, cycle_rate;
	Vector3 spawn_point;
	int spawn_x, spawn_y;
	GameObject spawn_object;
	GameObject instance_object;
	GameObject timer_panel;

	public override void GetMove()
	{
		if ((GetComponent<SwitchEndPoint>() == null) || (GetComponent<SwitchEndPoint>().SwitchOn))
		{
			// If a slime is not currently spawned
			if (instance_object == null)
			{
				// Decrement countdown
				moves_till_spawn--;

				UpdateTimer(moves_till_spawn);

				// If countdown at 0
				if (moves_till_spawn <= 0)
				{
					// Reset the countdown
					moves_till_spawn = cycle_rate;

					// If the space is not occupied by an enemy
					if (!EnemyGridControl.IsEnemyOccupied(spawn_x, spawn_y))
					{
						// If the space is not occupied by the player
						if ((PlayerLocator.Player_Pos_X != spawn_x) || (PlayerLocator.Player_Pos_Y != spawn_y))
						{
							// If the space is not occupied by a marker
							if (!MarkerControl.IsMarkerOccupied(spawn_x, spawn_y))
							{
								// Spawn the slime enemy
								instance_object = GameObject.Instantiate(spawn_object, spawn_point, Quaternion.identity);
								instance_object.GetComponent<BaseEnemyController>().SetEnemyPosition(spawn_x, spawn_y);
								EnemyGridControl.EnemyAdd(instance_object, spawn_x, spawn_y);
								UpdateTimer("spawn");
							}
							else
							{
								Debug.Log("(" + spawn_x.ToString() + ", " + spawn_y.ToString() + ") occupied by marker. Spawn failed.");
								UpdateTimer("fail");
							}
						}
						else
						{
							Debug.Log("(" + spawn_x.ToString() + ", " + spawn_y.ToString() + ") occupied by player. Spawn failed.");
							UpdateTimer("fail");
						}
					}
					else
					{
						Debug.Log("(" + spawn_x.ToString() + ", " + spawn_y.ToString() + ") occupied by enemy. Spawn failed.");
						UpdateTimer("fail");
					}
				}
			}
			//else
			//{
			//	UpdateTimer("pause");
			//}
		}
	}

	public void Initialize(int n_cycle_rate, int n_position_x, int n_position_y, int n_spawn_x, int n_spawn_y, GameObject n_slime)
	{
		transform.position = new Vector3((n_position_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_position_x, n_position_y) - 0.5f, (n_position_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
		spawn_point = new Vector3((n_spawn_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_spawn_x, n_spawn_y) - 1.0f, (n_spawn_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
		MoveComplete = true;
		moves_till_spawn = n_cycle_rate;
		cycle_rate = n_cycle_rate;
		spawn_x = n_spawn_x;
		spawn_y = n_spawn_y;
		spawn_object = n_slime;
		spawn_object.GetComponent<SimpleSlimeController>().spawn_source = this;

		timer_panel = GameObject.Instantiate(InterfaceSingleton.singleton.timerFrame_prefab, GameObject.Find("TimerPanelArea").transform);
		
		UpdateTimer(moves_till_spawn);
		ForceUpdate();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void Reset()
	{
		// Reset the countdown
		moves_till_spawn = cycle_rate;

		UpdateTimer(moves_till_spawn);
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

	public override void EndTurnUpdate()
	{
		//	if (instance_object == null)
		//	{
		//		UpdateTimer(moves_till_spawn);

		//		Debug.Log("Forced to " + moves_till_spawn);
		//	}
	}

	public override void ForceUpdate()
	{
		if (GetComponent<SwitchEndPoint>() == null)
		{
			timer_panel.transform.Find("Switch/SwitchImage").GetComponent<Image>().color = Color.clear;
			timer_panel.transform.Find("Switch").GetComponent<Image>().color = Color.clear;
			timer_panel.transform.Find("SwitchFrame").GetComponent<Image>().color = Color.clear;
		}
		else if (GetComponent<SwitchEndPoint>().SwitchOn)
		{
			timer_panel.transform.Find("Switch/SwitchImage").GetComponent<Image>().color = Color.green;
			timer_panel.transform.Find("Switch").GetComponent<Image>().color = Color.green;
			timer_panel.transform.Find("SwitchFrame").GetComponent<Image>().color = Color.white;
		}
		else
		{
			timer_panel.transform.Find("Switch/SwitchImage").GetComponent<Image>().color = Color.red;
			timer_panel.transform.Find("Switch").GetComponent<Image>().color = Color.red;
			timer_panel.transform.Find("SwitchFrame").GetComponent<Image>().color = Color.white;
		}
	}
}

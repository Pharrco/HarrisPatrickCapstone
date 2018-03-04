using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : BaseEnvironmentController
{
	int moves_till_spawn, cycle_rate;
	Vector3 spawn_point;
	int spawn_x, spawn_y;
	GameObject spawn_object;

    public override void GetMove()
	{
		// Decrement countdown
		moves_till_spawn--;

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
					// Spawn the slime enemy
					GameObject new_slime = GameObject.Instantiate(spawn_object, spawn_point, Quaternion.identity);
					new_slime.GetComponent<BaseEnemyController>().SetEnemyPosition(spawn_x, spawn_y);
					EnemyGridControl.EnemyAdd(new_slime, spawn_x, spawn_y);
				}
				else
				{
					Debug.Log("(" + spawn_x.ToString() + ", " + spawn_y.ToString() + ") occupied by player. Spawn failed.");
				}
			}
			else
			{
				Debug.Log("(" + spawn_x.ToString() + ", " + spawn_y.ToString() + ") occupied by enemy. Spawn failed.");
			}
		}
	}

    public void Initialize(int n_cycle_rate, int n_position_x, int n_position_y, int n_spawn_x, int n_spawn_y, GameObject n_slime )
    {
        transform.position = new Vector3((n_position_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_position_x, n_position_y) - 0.5f, (n_position_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
		spawn_point = new Vector3((n_spawn_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_spawn_x, n_spawn_y) - 1.0f, (n_spawn_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
		MoveComplete = true;
		moves_till_spawn = n_cycle_rate;
		cycle_rate = n_cycle_rate;
		spawn_x = n_spawn_x;
		spawn_y = n_spawn_y;
		spawn_object = n_slime;
    }

    // Update is called once per frame
    void Update () {
		
	}
}

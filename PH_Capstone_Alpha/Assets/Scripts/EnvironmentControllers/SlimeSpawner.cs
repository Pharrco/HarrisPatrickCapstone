using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : BaseEnvironmentController
{
    public override void GetMove() { }

    public void Initialize(int n_cycle_rate, int n_position_x, int n_position_y, int n_spawn_x, int n_spawn_y, GameObject n_slime )
    {
        transform.position = new Vector3((n_position_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_position_x, n_position_y) - 0.5f, (n_position_y - (BuildBoard.GetArrayWidth() / 2)) * 5);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

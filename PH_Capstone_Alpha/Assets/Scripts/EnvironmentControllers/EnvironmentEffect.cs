using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEffect
{
    public virtual void Trigger()
    { }
}

public class SimpleSlimeSpawner : EnvironmentEffect
{
    int spawn_x, spawn_y, cycle_rate, position_x, position_y;

    public SimpleSlimeSpawner(int n_cycle_rate, int n_position_x, int n_position_y, int n_spawn_x, int n_spawn_y)
    {
        cycle_rate = n_cycle_rate; // The number of turns between activation
        position_x = n_position_x; // The x position of the spawner (Cauldron)
        position_y = n_position_y; // The y position of the spawner (Cauldron)
        spawn_x = n_spawn_x; // The x position where a new slime will be spawned
        spawn_y = n_spawn_y; // The y position where a new slime will be spawned
    }

    public override void Trigger()
    {
        // Instantiate a clone of the slime spawner controller
        GameObject n_object = GameObject.Instantiate(EnvironmentSingleton.singleton.slime_spawner);

        // Set the cycle rate, cauldron position, slime spawn position, and slime to be spawned
        n_object.GetComponent<SlimeSpawner>().Initialize(cycle_rate, position_x, position_y, spawn_x, spawn_y, EnemySingleton.singleton.simple_slime);

        // Add the spawner to the environment list
        EnvironmentController.AddEnvironmentObject(n_object, position_x, position_y);
    }
}

public class GorgonDirectionalSpawn : EnvironmentEffect
{
	Enumerators.CompassDirect gorgon_placement;

	public GorgonDirectionalSpawn(Enumerators.CompassDirect n_direction)
	{
		gorgon_placement = n_direction; // The direction in the level the gorgon should be placed, relative to the board. This is also the direction that the player will be paralyzed if they move in this direction
	}

	public override void Trigger()
	{
		// Instantiate a clone of the directional gorgon object
		GameObject n_object = GameObject.Instantiate(EnvironmentSingleton.singleton.directional_gorgon);

		// Send the direction to the gorgon script, triggering the gorgon to adjust position and set behavior
		n_object.GetComponent<DirectionalGorgon>().Initialize(gorgon_placement);

		// Add the gorgon to the environmental list
		EnvironmentController.AddEnvironmentObject(n_object);
	}
}
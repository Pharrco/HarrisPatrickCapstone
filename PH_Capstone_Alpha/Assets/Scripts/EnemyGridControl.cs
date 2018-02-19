using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridControl : MonoBehaviour {

    static GameObject[,] enemy_array;
    static List<GameObject> enemy_list;

    // Use this for initialization
    void Start () {
        enemy_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        enemy_list = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		// If the current phase is the player result phase
        if (PhaseController.GetCurrPhase() == PhaseController.GamePhase.PlayerResult)
        {
            // If there is an enemy currently on the same space as the player
            if (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y] != null)
            {
                // Trigger the player on effect of the enemy controller
                enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().PlayerOn();
            }

            // TEMP: SET LIGHT RANGE
            int light_range = 1;

            // For 1 to range
            for (int i = 1; i <= light_range; i++)
            {
                // Trigger light effect in (x-i,y)
                if ((PlayerLocator.Player_Pos_X - i >= 0) && (enemy_array[PlayerLocator.Player_Pos_X - i, PlayerLocator.Player_Pos_Y] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X - i, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                }

                // Trigger light effect in (x+i,y)
                if ((PlayerLocator.Player_Pos_X + i < BuildBoard.GetArrayHeight()) && (enemy_array[PlayerLocator.Player_Pos_X + i, PlayerLocator.Player_Pos_Y] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X + i, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                }

                // Trigger light effect in (x,y-i)
                if ((PlayerLocator.Player_Pos_Y - i >= 0) && (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y - i] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y - i].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                }

                // Trigger light effect in (x,y+i)
                if ((PlayerLocator.Player_Pos_Y + i < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y + i] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y + i].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                }
            }

            light_range += 1;

            // Trigger no light effect in (x-i,y)
            if ((PlayerLocator.Player_Pos_X - light_range >= 0) && (enemy_array[PlayerLocator.Player_Pos_X - light_range, PlayerLocator.Player_Pos_Y] != null))
            {
                enemy_array[PlayerLocator.Player_Pos_X - light_range, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
            }

            // Trigger no light effect in (x+i,y)
            if ((PlayerLocator.Player_Pos_X + light_range < BuildBoard.GetArrayHeight()) && (enemy_array[PlayerLocator.Player_Pos_X + light_range, PlayerLocator.Player_Pos_Y] != null))
            {
                enemy_array[PlayerLocator.Player_Pos_X + light_range, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
            }

            // Trigger no light effect in (x,y-i)
            if ((PlayerLocator.Player_Pos_Y - light_range >= 0) && (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y - light_range] != null))
            {
                enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y - light_range].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
            }

            // Trigger no light effect in (x,y+i)
            if ((PlayerLocator.Player_Pos_Y + light_range < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y + light_range] != null))
            {
                enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y + light_range].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
            }

            // End the result phase
            PhaseController.EndPlayerResult();
        }
	}

    // Add enemy to list
    public static void EnemyAdd(GameObject n_enemy)
    {
        // Add this enemy to the list
        enemy_list.Add(n_enemy);
    }

    // Adds an enemy to both the list and the grid
    public static void EnemyAdd(GameObject n_enemy, int n_enemy_x, int n_enemy_y)
    {
        // If the provided coordinates are valid
        if ((n_enemy_x == Mathf.Clamp(n_enemy_x, 0, BuildBoard.GetArrayHeight())) && (n_enemy_y == Mathf.Clamp(n_enemy_y, 0, BuildBoard.GetArrayWidth())))
        {
            // If an object does not already exist in this space
            if (enemy_array[n_enemy_x, n_enemy_y] == null)
            {
                // Place the enemy on the appropriate place on the grid
                enemy_array[n_enemy_x, n_enemy_y] = n_enemy;
            }
            else
            {
                // Error: Attempted overwrite
                Debug.Log("Enemy spawn attempted to overwrite existing");
            }
        }
        else
        {
            // Error: Attempted to write off board
            Debug.Log("Enemy spawn attempted out of range");
        }

        // Add this enemy to the list
        enemy_list.Add(n_enemy);
    }

    public static void DestroyEnemyAt(int target_x, int target_y)
    {
        // If the provided coordinates are valid
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight())) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth())))
        {
            // If there is an enemy currently in the target space
            if (enemy_array[target_x, target_y] != null)
            {
                // Store the enemy
                GameObject hold_enemy = enemy_array[target_x, target_y];

                // Remove the enemy from the list
                enemy_list.Remove(hold_enemy);

                // Remove the enemy from the grid
                enemy_array[target_x, target_y] = null;

                // Destroy the enemy
                GameObject.Destroy(hold_enemy);
            }
        }
    }

    // Removes enemy from list (ONLY USE IF NOT ON GRID)
    public static void DestroyEnemy(GameObject target_enemy)
    {
        // Remove the enemy from the list
        enemy_list.Remove(target_enemy);

        // Destroy the enemy
        GameObject.Destroy(target_enemy);
    }
}

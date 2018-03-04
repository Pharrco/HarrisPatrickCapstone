using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridControl : MonoBehaviour
{

    static GameObject[,] enemy_array;
    static List<GameObject> enemy_list;

    // Use this for initialization
    public static void InitializeGrid()
    {
        enemy_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        enemy_list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the current phase is the player result phase
        if (PhaseController.GetCurrPhase() == GamePhase.PlayerResult)
        {
            // If there is an enemy currently on the same space as the player
            if (enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y] != null)
            {
                // Trigger the player on effect of the enemy controller
                enemy_array[PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y].GetComponent<BaseEnemyController>().PlayerOn();
            }

            // Set light range
            int light_range = LightResourceControl.GetLightRange();

            // For 1 to range
            for (int i = 1; i <= light_range; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int major_dev = i - j;
                    int minor_dev = j;

                    LightEffectControl.SetLightGridPoint(PlayerLocator.Player_Pos_X, PlayerLocator.Player_Pos_Y, LightResourceControl.Player_LightStatus);
                    LightEffectControl.SetLightGridPoint(PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev, LightResourceControl.Player_LightStatus);
                    LightEffectControl.SetLightGridPoint(PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev, LightResourceControl.Player_LightStatus);
                    LightEffectControl.SetLightGridPoint(PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev, LightResourceControl.Player_LightStatus);
                    LightEffectControl.SetLightGridPoint(PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev, LightResourceControl.Player_LightStatus);

                    if ((PlayerLocator.Player_Pos_X - major_dev >= 0) && (PlayerLocator.Player_Pos_Y - minor_dev >= 0) && (enemy_array[PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev] != null))
                    {
                        enemy_array[PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                    }

                    if ((PlayerLocator.Player_Pos_X + major_dev < BuildBoard.GetArrayHeight()) && (PlayerLocator.Player_Pos_Y + minor_dev < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev] != null))
                    {
                        enemy_array[PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                    }

                    if ((PlayerLocator.Player_Pos_X - minor_dev >= 0) && (PlayerLocator.Player_Pos_Y + major_dev < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev] != null))
                    {
                        enemy_array[PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                    }

                    if ((PlayerLocator.Player_Pos_X + minor_dev < BuildBoard.GetArrayHeight()) && (PlayerLocator.Player_Pos_Y - major_dev >= 0) && (enemy_array[PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev] != null))
                    {
                        enemy_array[PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev].GetComponent<BaseEnemyController>().LightEffect(LightResourceControl.Player_LightStatus);
                    }
                }
            }

            int k = light_range + 1;

            for (int j = 0; j < k; j++)
            {
                int major_dev = k - j;
                int minor_dev = j;

                LightEffectControl.ResetLightGridPoint(PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev);
                LightEffectControl.ResetLightGridPoint(PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev);
                LightEffectControl.ResetLightGridPoint(PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev);
                LightEffectControl.ResetLightGridPoint(PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev);

                if ((PlayerLocator.Player_Pos_X - major_dev >= 0) && (PlayerLocator.Player_Pos_Y - minor_dev >= 0) && (enemy_array[PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X - major_dev, PlayerLocator.Player_Pos_Y - minor_dev].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
                }

                if ((PlayerLocator.Player_Pos_X + major_dev < BuildBoard.GetArrayHeight()) && (PlayerLocator.Player_Pos_Y + minor_dev < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X + major_dev, PlayerLocator.Player_Pos_Y + minor_dev].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
                }

                if ((PlayerLocator.Player_Pos_X - minor_dev >= 0) && (PlayerLocator.Player_Pos_Y + major_dev < BuildBoard.GetArrayWidth()) && (enemy_array[PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X - minor_dev, PlayerLocator.Player_Pos_Y + major_dev].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
                }

                if ((PlayerLocator.Player_Pos_X + minor_dev < BuildBoard.GetArrayHeight()) && (PlayerLocator.Player_Pos_Y - major_dev >= 0) && (enemy_array[PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev] != null))
                {
                    enemy_array[PlayerLocator.Player_Pos_X + minor_dev, PlayerLocator.Player_Pos_Y - major_dev].GetComponent<BaseEnemyController>().LightEffect(LightStatus.Nopwr);
                }
            }

            // End the result phase
            PhaseController.EndPlayerResult();
        }
        // If the current phase is the enemy turn phase
        if (PhaseController.GetCurrPhase() == GamePhase.EnemyTurn)
        {
            // For each enemy on the list
            foreach (GameObject enemy_object in enemy_list)
            {
                // If an enemy controller is present
                if (enemy_object.GetComponent<BaseEnemyController>() != null)
                {
                    enemy_object.GetComponent<BaseEnemyController>().GetMove();
                }
            }

            // End enemy turn, begin animation state
            PhaseController.EndEnemyTurn();
        }
		// If the current phase is the enemy animation phase
        if (PhaseController.GetCurrPhase() == GamePhase.EnemyAnimation)
        {
            // Assume all movements are complete
            bool movements_complete = true;

            // For each enemy on the list
            foreach (GameObject enemy_object in enemy_list)
            {
                // If an enemy controller is present
                if (enemy_object.GetComponent<BaseEnemyController>() != null)
                {
                    movements_complete = (movements_complete && enemy_object.GetComponent<BaseEnemyController>().MoveComplete);
                }
            }

            if (movements_complete)
            {
                PhaseController.EndEnemyAnimation();
            }
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
        if ((n_enemy_x == Mathf.Clamp(n_enemy_x, 0, BuildBoard.GetArrayHeight() - 1)) && (n_enemy_y == Mathf.Clamp(n_enemy_y, 0, BuildBoard.GetArrayWidth() - 1)))
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
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
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

    public static void ResetEnemyGrid()
    {
        // Clear the grid
        enemy_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

        // Destroy all enemies
        while (enemy_list.Count > 0)
        {
            GameObject temp = enemy_list[0];

            enemy_list.Remove(temp);

            GameObject.Destroy(temp);
        }

        // Reset the list
        enemy_list = new List<GameObject>();
    }

    public static bool IsEnemyOccupied(int target_x, int target_y)
    {
        // If the provided coordinates are valid
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
        {
            // If there is an enemy currently in the target space
            if (enemy_array[target_x, target_y] != null)
            {
                return true;
            }
        }

        return false;
    }

    public static void SwapEnemyPoints(int origin_x, int origin_y, int destination_x, int destination_y)
    {
        // If the provided origin coordinates are valid
        if ((origin_x == Mathf.Clamp(origin_x, 0, BuildBoard.GetArrayHeight() - 1)) && (origin_y == Mathf.Clamp(origin_y, 0, BuildBoard.GetArrayWidth() - 1)))
        {
            // If there is an enemy currently in the origin space
            if (enemy_array[origin_x, origin_y] != null)
            {
                // If the provided destination coordinates are valid
                if ((destination_x == Mathf.Clamp(destination_x, 0, BuildBoard.GetArrayHeight() - 1)) && (destination_y == Mathf.Clamp(destination_y, 0, BuildBoard.GetArrayWidth() - 1)))
                {
                    // If there is not an enemy currently in the target space
                    if (enemy_array[destination_x, destination_y] == null)
                    {
                        // Swap object's position in grid
                        enemy_array[destination_x, destination_y] = enemy_array[origin_x, origin_y];
                        enemy_array[origin_x, origin_y] = null;

                        // Send new coordinates to moved object
                        enemy_array[destination_x, destination_y].GetComponent<BaseEnemyController>().SetEnemyPosition(destination_x, destination_y);
                    }
                    else
                    {
                        Debug.Log("Enemy Grid Swap Error: Destination occupied");
                    }
                }
                else
                {
                    Debug.Log("Enemy Grid Swap Error: Destination out of range");
                }
            }
            else
            {
                Debug.Log("Enemy Grid Swap Error: No object at origin");
            }
        }
        else
        {
            Debug.Log("Enemy Grid Swap Error: Origin out of range");
        }
    }
}

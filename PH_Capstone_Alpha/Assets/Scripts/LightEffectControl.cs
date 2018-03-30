using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffectControl : MonoBehaviour {

	static Dictionary<LightStatus, GameObject> light_effect_dictionary;
    static GameObject[,] light_effect_grid;
    static LightStatus[,] light_status_grid;

    // Use this for initialization
    public static void InitializeLightGrid(GameObject n_lightEffect_white, GameObject n_lightEffect_nopwr, GameObject n_lightEffect_infrd, GameObject n_lightEffect_ulvlt) {
        // Initialize the grid
        light_effect_grid = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
        light_status_grid = new LightStatus[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
		light_effect_dictionary = new Dictionary<LightStatus, GameObject>();

		light_effect_dictionary.Add(LightStatus.White, n_lightEffect_white);
		light_effect_dictionary.Add(LightStatus.Infrd, n_lightEffect_infrd);
		light_effect_dictionary.Add(LightStatus.Nopwr, n_lightEffect_nopwr);
		light_effect_dictionary.Add(LightStatus.Ulvlt, n_lightEffect_ulvlt);
		
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				if (BuildBoard.GetArrayValue(i, j) > 0)
				{
					if (light_effect_grid[i, j] == null)
					{
						light_effect_grid[i, j] = GameObject.Instantiate(light_effect_dictionary[LightStatus.Nopwr], new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 1f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
					}

					light_status_grid[i, j] = LightStatus.Nopwr;
				}
			}
		}
	}
	
    public static void ResetLightGridPoint(int target_x, int target_y)
    {
        // If the provided coordinates are valid
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
        {
			if (BuildBoard.GetArrayValue(target_x, target_y) > 0)
			{
				// If the current status is not already no power
				if (light_status_grid[target_x, target_y] != LightStatus.Nopwr)
				{
					// If an object already exists in this space
					if (light_effect_grid[target_x, target_y] != null)
					{
						// Get the object
						GameObject temp = light_effect_grid[target_x, target_y];

						// Clear the grid
						light_effect_grid[target_x, target_y] = null;

						// Destroy the object
						GameObject.Destroy(temp);

						// Light status is no power
						light_status_grid[target_x, target_y] = LightStatus.Nopwr;
					}

					// Create a new light object at the given position
					light_effect_grid[target_x, target_y] = GameObject.Instantiate(light_effect_dictionary[LightStatus.Nopwr], new Vector3((target_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(target_x, target_y) - 1f, (target_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
				}
			}
		}
    }

    public static void SetLightGridPoint(int target_x, int target_y, LightStatus n_status)
    {
        // If the provided coordinates are valid
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
        {
			if (BuildBoard.GetArrayValue(target_x, target_y) > 0)
			{
				// If an object already exists in this space
				if (light_effect_grid[target_x, target_y] != null)
				{
					// Get the object
					GameObject temp = light_effect_grid[target_x, target_y];

					// Clear the grid
					light_effect_grid[target_x, target_y] = null;

					// Destroy the object
					GameObject.Destroy(temp);
				}

				// Light status set
				light_status_grid[target_x, target_y] = LightResourceControl.Player_LightStatus;

				// Create a new light object at the given position
				light_effect_grid[target_x, target_y] = GameObject.Instantiate(light_effect_dictionary[n_status], new Vector3((target_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(target_x, target_y) - 1f, (target_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
			}
		}
    }

    public static void ResetLightGrid()
    {
        for (int i = 0; i < light_effect_grid.GetLength(0); i++)
        {
            for (int j = 0; j < light_effect_grid.GetLength(1); j++)
            {
				if (BuildBoard.GetArrayValue(i, j) > 0)
				{
					if (light_effect_grid[i, j] != null)
					{
						GameObject.Destroy(light_effect_grid[i, j]);

						light_effect_grid[i, j] = GameObject.Instantiate(light_effect_dictionary[LightStatus.Nopwr], new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 1f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
					}

					light_status_grid[i, j] = LightStatus.Nopwr;
				}
            }
        }
    }

    public static LightStatus GetLightPoint(int target_x, int target_y)
    {
        // If the provided coordinates are valid
        if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
        {
            return light_status_grid[target_x, target_y]; 
        }

        return LightStatus.Nopwr;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyGridControl : MonoBehaviour {

	static GameObject[,] ally_array;
	static List<GameObject> ally_list;

	// Use this for initialization
	public static void InitializeGrid()
	{
		ally_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
		ally_list = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		// If the current phase is the ally turn phase
		if (PhaseController.GetCurrPhase() == GamePhase.AllyTurn)
		{
			// For each ally on the list
			foreach (GameObject ally_object in ally_list)
			{
				// If an ally controller is present
				if (ally_object.GetComponent<BaseAllyController>() != null)
				{
					ally_object.GetComponent<BaseAllyController>().GetMove();
				}
			}

			// End ally turn, begin animation state
			PhaseController.EndAllyTurn();
		}
		// If the current phase is the ally animation phase
		if (PhaseController.GetCurrPhase() == GamePhase.AllyAnimation)
		{
			// Assume all movements are complete
			bool movements_complete = true;

			// For each ally on the list
			foreach (GameObject ally_object in ally_list)
			{
				// If an ally controller is present
				if (ally_object.GetComponent<BaseAllyController>() != null)
				{
					movements_complete = (movements_complete && ally_object.GetComponent<BaseAllyController>().MoveComplete);
				}
			}

			if (movements_complete)
			{
				PhaseController.EndAllyAnimation();
			}
		}
	}

	// Add ally to list
	public static void AllyAdd(GameObject n_ally)
	{
		// Add this ally to the list
		ally_list.Add(n_ally);
	}

	// Adds an ally to both the list and the grid
	public static void AllyAdd(GameObject n_ally, int n_ally_x, int n_ally_y)
	{
		// If the provided coordinates are valid
		if ((n_ally_x == Mathf.Clamp(n_ally_x, 0, BuildBoard.GetArrayHeight() - 1)) && (n_ally_y == Mathf.Clamp(n_ally_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If an object does not already exist in this space
			if (ally_array[n_ally_x, n_ally_y] == null)
			{
				// Place the ally on the appropriate place on the grid
				ally_array[n_ally_x, n_ally_y] = n_ally;
			}
			else
			{
				// Error: Attempted overwrite
				Debug.Log("Ally spawn attempted to overwrite existing");
			}
		}
		else
		{
			// Error: Attempted to write off board
			Debug.Log("Ally spawn attempted out of range");
		}

		// Add this ally to the list
		ally_list.Add(n_ally);
	}

	public static void DestroyAllyAt(int target_x, int target_y)
	{
		// If the provided coordinates are valid
		if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If there is an ally currently in the target space
			if (ally_array[target_x, target_y] != null)
			{
				// Store the ally
				GameObject hold_ally = ally_array[target_x, target_y];

				// Remove the ally from the list
				ally_list.Remove(hold_ally);

				// Remove the ally from the grid
				ally_array[target_x, target_y] = null;

				// Destroy the ally
				GameObject.Destroy(hold_ally);
			}
		}
	}

	// Removes ally from list (ONLY USE IF NOT ON GRID)
	public static void DestroyAlly(GameObject target_ally)
	{
		// Remove the ally from the list
		ally_list.Remove(target_ally);

		// Destroy the ally
		GameObject.Destroy(target_ally);
	}

	public static void ResetAllyGrid()
	{
		// Clear the grid
		ally_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

		// Destroy all enemies
		while (ally_list.Count > 0)
		{
			GameObject temp = ally_list[0];

			ally_list.Remove(temp);

			GameObject.Destroy(temp);
		}

		// Reset the list
		ally_list = new List<GameObject>();
	}

	public static bool IsAllyOccupied(int target_x, int target_y)
	{
		// If the provided coordinates are valid
		if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If there is an ally currently in the target space
			if (ally_array[target_x, target_y] != null)
			{
				return true;
			}
		}

		return false;
	}

	public static void SwapAllyPoints(int origin_x, int origin_y, int destination_x, int destination_y)
	{
		// If the provided origin coordinates are valid
		if ((origin_x == Mathf.Clamp(origin_x, 0, BuildBoard.GetArrayHeight() - 1)) && (origin_y == Mathf.Clamp(origin_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If there is an ally currently in the origin space
			if (ally_array[origin_x, origin_y] != null)
			{
				// If the provided destination coordinates are valid
				if ((destination_x == Mathf.Clamp(destination_x, 0, BuildBoard.GetArrayHeight() - 1)) && (destination_y == Mathf.Clamp(destination_y, 0, BuildBoard.GetArrayWidth() - 1)))
				{
					// If there is not an ally currently in the target space
					if (ally_array[destination_x, destination_y] == null)
					{
						// Swap object's position in grid
						ally_array[destination_x, destination_y] = ally_array[origin_x, origin_y];
						ally_array[origin_x, origin_y] = null;

						// Send new coordinates to moved object
						ally_array[destination_x, destination_y].GetComponent<BaseAllyController>().SetAllyPosition(destination_x, destination_y);
					}
					else
					{
						Debug.Log("Ally Grid Swap Error: Destination occupied");
					}
				}
				else
				{
					Debug.Log("Ally Grid Swap Error: Destination out of range");
				}
			}
			else
			{
				Debug.Log("Ally Grid Swap Error: No object at origin");
			}
		}
		else
		{
			Debug.Log("Ally Grid Swap Error: Origin out of range");
		}
	}

	public static GameObject GetAllyFromGrid(int target_x, int target_y)
	{
		// If the provided coordinates are valid
		if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If there is an ally currently in the target space
			if (ally_array[target_x, target_y] != null)
			{
				// Return the game object stored in the array
				return ally_array[target_x, target_y];
			}
		}

		return null;
	}
}

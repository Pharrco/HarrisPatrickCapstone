using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

	static List<GameObject> environment_list;
	static GameObject[,] environment_array;

	// Use this for initialization
	public static void Initialize()
	{
		environment_list = new List<GameObject>();
		environment_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
	}

	public static void AddEnvironmentObject(GameObject n_object)
	{
		// If the list exists
		if (environment_list != null)
		{
			// If the object being added is not null and has a valid component
			if ((n_object != null) && (n_object.GetComponent<BaseEnvironmentController>() != null))
			{
				// Add the object to the list
				environment_list.Add(n_object);
			}
			else
			{
				// Error: Attempted to write invalid object
				Debug.Log("Environment spawn attempted invalid object");
			}
		}
		else
		{
			// Error: Attempted to write to non-existant list
			Debug.Log("Environment spawn attempted out of range");
		}
	}

	public static void AddEnvironmentObject(GameObject n_object, int n_environment_x, int n_environment_y)
	{
		// If the grid exists
		if (environment_array != null)
		{
			// If the object being added is not null and has a valid component
			if ((n_object != null) && (n_object.GetComponent<BaseEnvironmentController>() != null))
			{
				// If the provided coordinates are valid
				if ((n_environment_x == Mathf.Clamp(n_environment_x, 0, BuildBoard.GetArrayHeight() - 1)) && (n_environment_y == Mathf.Clamp(n_environment_y, 0, BuildBoard.GetArrayWidth() - 1)))
				{
					// If an object does not already exist in this space
					if (environment_array[n_environment_x, n_environment_y] == null)
					{
						// Place the environment object on the appropriate place on the grid
						environment_array[n_environment_x, n_environment_y] = n_object;
					}
					else
					{
						// Error: Attempted overwrite
						Debug.Log("Environment spawn attempted to overwrite existing");
					}
				}
				else
				{
					// Error: Attempted to write off board
					Debug.Log("Environment spawn attempted out of range");
				}
			}
			else
			{
				// Error: Attempted to write invalid object
				Debug.Log("Environment spawn attempted invalid object");
			}
		}
		else
		{
			// Error: Attempted to write to non-existant board
			Debug.Log("Environment spawn attempted on null grid");
		}

		// Recurse to add to list
		AddEnvironmentObject(n_object);
	}

	public static bool IsOccupied(int target_x, int target_y)
	{
		// If the provided coordinates are valid
		if ((target_x == Mathf.Clamp(target_x, 0, BuildBoard.GetArrayHeight() - 1)) && (target_y == Mathf.Clamp(target_y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If there is an enemy currently in the target space
			if (environment_array[target_x, target_y] != null)
			{
				return true;
			}
		}

		return false;
	}

	// Update is called once per frame
	void Update()
	{
		// If the current phase is the environment turn phase
		if (PhaseController.GetCurrPhase() == GamePhase.EnvironmentTurn)
		{
			// For each environment on the list
			foreach (GameObject environment_object in environment_list)
			{
				// If an environment controller is present
				if (environment_object.GetComponent<BaseEnvironmentController>() != null)
				{
					environment_object.GetComponent<BaseEnvironmentController>().GetMove();
				}
			}

			// End environment turn, begin animation state
			PhaseController.EndEnvironmentTurn();
		}
		// If the current phase is the environment animation phase
		if (PhaseController.GetCurrPhase() == GamePhase.EnvironmentResult)
		{
			// Assume all movements are complete
			bool movements_complete = true;

			// For each environment on the list
			foreach (GameObject environment_object in environment_list)
			{
				// If an environment controller is present
				if (environment_object.GetComponent<BaseEnvironmentController>() != null)
				{
					movements_complete = (movements_complete && environment_object.GetComponent<BaseEnvironmentController>().MoveComplete);
				}
			}

			if (movements_complete)
			{
				PhaseController.EndEnvironmentResult();
			}
		}
	}

	static public void ResetAllEnvironment()
	{
		// For each environment effect in the level
		foreach (GameObject environment_object in environment_list)
		{
			// Reset the effect
			environment_object.GetComponent<BaseEnvironmentController>().Reset();
		}
	}
}

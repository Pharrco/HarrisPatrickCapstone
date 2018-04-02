using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerControl : MonoBehaviour
{

	[SerializeField]
	GameObject n_active_marker, n_inactive_marker, n_simple_marker;
	static GameObject active_marker, inactive_marker, simple_marker;
	static int[,] board_marker_array;
	static GameObject[,] marker_array;
	[SerializeField]
	Text n_markers_text;
	static Text markers_text;
	static Image markers_fill;
	static int markers_total, markers_passed, markers_new, markers_handicap;
	static GameObject marker_WinIndicator, marker_HandIndicator;

	// Use this for initialization
	public void Start()
	{
		// Make serialized objects to static
		active_marker = n_active_marker;
		simple_marker = n_simple_marker;
		inactive_marker = n_inactive_marker;
		markers_text = n_markers_text;

		markers_fill = GameObject.Find("MagicFill").GetComponent<Image>();
		marker_WinIndicator = GameObject.Find("MagicIcon");
		marker_HandIndicator = GameObject.Find("HandicapIndicator");

		marker_HandIndicator.GetComponent<Image>().color = Color.clear;

		// Reset marker counts
		markers_passed = 0;
		markers_total = 0;
		markers_new = 0;
		markers_handicap = 0;

		// Initialize the integer and object array
		board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
		marker_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

		// For each column
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			// For each entry in the column
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				board_marker_array[i, j] = -1;
			}
		}

		foreach (Vector2 coordinate in GetComponent<LevelBase>().Marker_list)
		{
			board_marker_array[(int)coordinate.x, (int)coordinate.y] = 0;
		}

		// For each column
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			// For each entry in the column
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				bool not_empty = true;

				if ((GetComponent<LevelBase>().Empty_list != null) && (GetComponent<LevelBase>().Empty_list.Contains(new Vector2(i, j))))
				{
					not_empty = false;
				}

				if (not_empty)
				{
					// If space has not been designated as empty or special
					if (board_marker_array[i, j] == 0)
					{
						// Add to the total pre-marker count
						markers_total += 1;

						// Instantiate the pre-marker
						marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.4f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));

						// Mark the mini map
						GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(i, j, Color.yellow);
					}
					// If space has not been designated as empty or special
					else if (board_marker_array[i, j] == -1)
					{
						if ((BuildBoard.GetArrayValue(i, j) > 0) && !((i == GetComponent<LevelBase>().Player_start.x) && (j == GetComponent<LevelBase>().Player_start.y)))
						{
							// Add to the total pre-marker count
							markers_total += 1;

							// Instantiate the pre-marker
							marker_array[i, j] = GameObject.Instantiate(simple_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.4f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));

							// Mark the mini map
							GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(i, j, Color.blue);
						}
					}
				}
			}
		}

		// Update the marker text
		markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
		UpdateHandicapVictoryUI();

		// Update the marker fill
		markers_fill.fillAmount = (float)(markers_total - markers_passed) / (float)markers_total;
	}

	// Mark the specified space, showing that a player has passed through the space and is no longer allowed to enter this space
	public static void MarkSpace(int x, int y)
	{
		// If space was occupied by a pre-marker
		if (board_marker_array[x, y] == 0)
		{
			// Add to the marked space total
			markers_passed += 1;

			// Update the marker text
			markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
			UpdateHandicapVictoryUI();

			// Update the marker fill
			markers_fill.fillAmount = (float)(markers_total - markers_passed) / (float)markers_total;

			// Destroy the pre-marker
			GameObject.Destroy(marker_array[x, y]);

			// Update the int array to show the space has been marked
			board_marker_array[x, y] = 1;

			// Instantiate the marker
			marker_array[x, y] = GameObject.Instantiate(active_marker, new Vector3((x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(x, y) - 0.5f, (y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.identity);

			// Mark the mini map
			GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(x, y, Color.black);
		}

		if ((board_marker_array[x, y] == -1) && (marker_array[x, y] != null))
		{
			// Add to the marked space total
			markers_passed += 1;

			// Update the marker text
			markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
			UpdateHandicapVictoryUI();

			// Update the marker fill
			markers_fill.fillAmount = (float)(markers_total - markers_passed) / (float)markers_total;

			// Destroy the pre-marker
			GameObject.Destroy(marker_array[x, y]);

			// Mark the mini map
			GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(x, y, Color.clear);
		}
	}

	// Returns whether the player can move to the space based on the marker state. Returns false if a space has been marked as impassible
	public static bool CanMove(int x, int y)
	{
		if (board_marker_array[x, y] <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	// Reset the level
	public void ResetMarkers()
	{
		// Reset marker counts
		markers_passed = 0;
		markers_total = 0;
		markers_new = 0;
		markers_handicap = 0;

		// For each column
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			// For each entry in the column
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				if (marker_array[i, j] != null)
				{
					GameObject.Destroy(marker_array[i, j]);
				}
			}
		}

		// Initialize the integer and object array
		board_marker_array = new int[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];
		marker_array = new GameObject[BuildBoard.GetArrayHeight(), BuildBoard.GetArrayWidth()];

		// For each column
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			// For each entry in the column
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				board_marker_array[i, j] = -1;
			}
		}

		foreach (Vector2 coordinate in GetComponent<LevelBase>().Marker_list)
		{
			board_marker_array[(int)coordinate.x, (int)coordinate.y] = 0;
		}

		// For each column
		for (int i = 0; i < BuildBoard.GetArrayHeight(); i++)
		{
			// For each entry in the column
			for (int j = 0; j < BuildBoard.GetArrayWidth(); j++)
			{
				bool not_empty = true;

				if ((GetComponent<LevelBase>().Empty_list != null) && (GetComponent<LevelBase>().Empty_list.Contains(new Vector2(i, j))))
				{
					not_empty = false;
				}

				if (not_empty)
				{
					// If space has not been designated as empty or special
					if (board_marker_array[i, j] == 0)
					{
						// Add to the total pre-marker count
						markers_total += 1;

						// Instantiate the pre-marker
						marker_array[i, j] = GameObject.Instantiate(inactive_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));

						// Mark the mini map
						GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(i, j, Color.yellow);
					}
					// If space has not been designated as empty or special
					else if (board_marker_array[i, j] == -1)
					{
						if ((BuildBoard.GetArrayValue(i, j) > 0) && !((i == GetComponent<LevelBase>().Player_start.x) && (j == GetComponent<LevelBase>().Player_start.y)))
						{
							// Add to the total pre-marker count
							markers_total += 1;

							// Instantiate the pre-marker
							marker_array[i, j] = GameObject.Instantiate(simple_marker, new Vector3((i - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(i, j) - 0.9f, (j - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));

							// Mark the mini map
							GameObject.Find("MiniMap").GetComponent<MinimapController>().SetMapMarker(i, j, Color.blue);
						}
					}
				}
			}
		}

		// Update the marker text
		markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
		UpdateHandicapVictoryUI();

		// Update the marker fill
		markers_fill.fillAmount = (float)(markers_total - markers_passed) / (float)markers_total;

		// Reset the enemy/ally arrays
		EnemyGridControl.ResetEnemyGrid();
		AllyGridControl.ResetAllyGrid();

		// Reset environment
		EnvironmentController.ResetAllEnvironment();

		if (GetComponent<LevelBase>().Enemy_Spawn_List != null)
		{
			foreach (EnemySpawn enemy_spawn in GetComponent<LevelBase>().Enemy_Spawn_List)
			{
				EnemyGridControl.EnemyAdd(enemy_spawn.TriggerSpawn(), enemy_spawn.GetX(), enemy_spawn.GetY());
			}
		}
		else
		{
			Debug.Log("No list detected");
		}

		// Reset the player's position and facing
		GetComponent<PlacePlayerCharacter>().ResetPlayer();

		// Reset the phase state
		PhaseController.ResetPhaseState();

		// Reset the light effect array
		LightEffectControl.ResetLightGrid();

		// Reset the light resource
		LightResourceControl.ResetLightResource();

		// Reset the play cash
		CashControl.ResetLevelCash();

		// Set light effect based on player start position
		LightEffectControl.SetLightGridPoint((int)GetComponent<LevelBase>().Player_start.x, (int)GetComponent<LevelBase>().Player_start.y, LightStatus.White);
		LightEffectControl.SetLightGridPoint((int)GetComponent<LevelBase>().Player_start.x - 1, (int)GetComponent<LevelBase>().Player_start.y, LightStatus.White);
		LightEffectControl.SetLightGridPoint((int)GetComponent<LevelBase>().Player_start.x + 1, (int)GetComponent<LevelBase>().Player_start.y, LightStatus.White);
		LightEffectControl.SetLightGridPoint((int)GetComponent<LevelBase>().Player_start.x, (int)GetComponent<LevelBase>().Player_start.y + 1, LightStatus.White);
		LightEffectControl.SetLightGridPoint((int)GetComponent<LevelBase>().Player_start.x, (int)GetComponent<LevelBase>().Player_start.y - 1, LightStatus.White);
	}

	public static bool LevelComplete()
	{
		if (markers_passed >= markers_total - markers_handicap)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	// Returns whether there is any marker at all in the position
	public static bool IsMarkerOccupied(int x, int y)
	{
		// If the provided coordinates are valid
		if ((x == Mathf.Clamp(x, 0, BuildBoard.GetArrayHeight() - 1)) && (y == Mathf.Clamp(y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			if (marker_array[x, y] != null)
			{
				return true;
			}
		}

		return false;
	}

	// Places a new simple marker in this position and updates totals
	public static void PlaceSimpleMarker(int x, int y)
	{
		// If the provided coordinates are valid
		if ((x == Mathf.Clamp(x, 0, BuildBoard.GetArrayHeight() - 1)) && (y == Mathf.Clamp(y, 0, BuildBoard.GetArrayWidth() - 1)))
		{
			// If the position is vacant of a marker
			if (marker_array[x, y] == null)
			{
				// Add to the total pre-marker count
				markers_total += 1;
				markers_new += 1;
				markers_handicap = markers_new / Constants.MARKER_HANDICAP_RATE;

				// Instantiate the pre-marker
				marker_array[x, y] = GameObject.Instantiate(simple_marker, new Vector3((x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(x, y) - 0.9f, (y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(90, 0, 0));
				
				// Update the marker text
				markers_text.text = markers_passed.ToString("00") + " / " + markers_total.ToString("00");
				UpdateHandicapVictoryUI();

				// Update the marker fill
				markers_fill.fillAmount = (float)(markers_total - markers_passed) / (float)markers_total;
			}
		}
	}

	public static void UpdateHandicapVictoryUI()
	{
		// If a handicap has not been set
		if (markers_handicap <= 0)
		{
			// Hide the handicap marker
			marker_HandIndicator.GetComponent<Image>().color = Color.clear;
		}
		// If a handicap has been set
		else
		{
			// Show the handicap marker
			marker_HandIndicator.GetComponent<Image>().color = Color.white;

			// Get the width of the fill bar
			float area_width = markers_fill.GetComponent<RectTransform>().rect.width;

			// Get the position of the handicap bar based on the amount of handicap markers of the total
			float handicap_pos = (area_width * ((float)markers_handicap / (float)markers_total)) - (area_width / 2.0f);

			// Set the bar based on handicap
			marker_HandIndicator.transform.localPosition = new Vector3(handicap_pos, 0, 0);
		}

		if (LevelComplete())
		{
			marker_WinIndicator.GetComponent<Image>().sprite = InterfaceSingleton.singleton.chargeAble_icon;
		}
		else
		{
			marker_WinIndicator.GetComponent<Image>().sprite = InterfaceSingleton.singleton.chargeUnable_icon;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour {

    public enum GamePhase { PlayerTurn, PlayerAnimation, PlayerResult, AllyTurn, AllyResult, EnemyTurn, EnemyResult, EnvironmentTurn, EnvironmentResult, TimedTestPhase}
    static GamePhase curr_phase;
    [SerializeField]
    float test_timer;
    static float time_elapsed_test = 0;

	// Use this for initialization
	void Start () {
        // Initial phase is player turn
        curr_phase = GamePhase.PlayerTurn;
	}
	
	// Update is called once per frame
	void Update () {
        // If the current game phase is the timed test phase
		if (curr_phase == GamePhase.TimedTestPhase)
        {
            // Increment timer
            time_elapsed_test += Time.deltaTime;

            // If time elapsed is greater than target time
            if (time_elapsed_test > test_timer)
            {
                // Reset the timer
                time_elapsed_test = 0;

                // Move to the player's turn
                curr_phase = GamePhase.PlayerTurn;
            }
        }
	}

    // End the player turn and move to the player animation phase
    public static void EndPlayerTurn()
    {
        curr_phase = GamePhase.PlayerAnimation;   
    }

    // End the player animation phase and move to the player turn phase
    public static void EndPlayerAnimation()
    {
        curr_phase = GamePhase.PlayerTurn;
    }

    // Return the current game phase for testing from any object
    public static GamePhase GetCurrPhase()
    {
        return curr_phase;
    }
}

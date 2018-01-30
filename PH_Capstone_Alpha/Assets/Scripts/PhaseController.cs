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
        curr_phase = GamePhase.PlayerTurn;
	}
	
	// Update is called once per frame
	void Update () {
		if (curr_phase == GamePhase.TimedTestPhase)
        {
            time_elapsed_test += Time.deltaTime;

            if (time_elapsed_test > test_timer)
            {
                time_elapsed_test = 0;
                curr_phase = GamePhase.PlayerTurn;
            }
        }
	}

    public static void EndPlayerTurn()
    {
        curr_phase = GamePhase.TimedTestPhase;
        time_elapsed_test = 0;
    }

    public static GamePhase GetCurrPhase()
    {
        return curr_phase;
    }
}

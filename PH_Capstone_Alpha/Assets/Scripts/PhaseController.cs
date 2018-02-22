using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GamePhase { PlayerTurn, PlayerAnimation, PlayerError, PlayerResult, AllyTurn, AllyResult, EnemyTurn, EnemyAnimation, EnemyResult, EnvironmentTurn, EnvironmentResult, TimedTestPhase, GameLose, GameVictory, GamePaused }

public class PhaseController : MonoBehaviour {

    public static GamePhase curr_phase;
    [SerializeField]
    float test_timer;
    static bool paused = false;
    static float time_elapsed_test = 0;
    static Canvas lose_panel, win_panel, menu_panel, message_panel;
    static bool pause_pPlayer, pause_pEnemy, pause_pAlly, pause_pEnvironment;

    // Use this for initialization
    void Start () {
        // Initial phase is player turn
        curr_phase = GamePhase.PlayerTurn;

        // Get the victory canvas
        win_panel = GameObject.Find("VictoryPanel").GetComponent<Canvas>();

        // Get the loss canvas
        lose_panel = GameObject.Find("LosePanel").GetComponent<Canvas>();

        // Get the menu canvas
        menu_panel = GameObject.Find("PausePanel").GetComponent<Canvas>();

        // Get the message canvas
        message_panel = GameObject.Find("MessagePanel").GetComponent<Canvas>();

        // Unpause the game
        paused = false;

        // Set no pauses
        pause_pPlayer = false;
        pause_pEnemy = false;
        pause_pAlly = false;
        pause_pEnvironment = false;
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

        if ((message_panel.planeDistance == 0.5f) && (Input.GetKeyDown(KeyCode.Return)))
        {
            EndMessagePause();
        }

        if ((message_panel.planeDistance != 0.5f) && (lose_panel.planeDistance != 0.5f) && (win_panel.planeDistance != 0.5f) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            TogglePause();
        }

    }

    // End the player turn and move to the player animation phase
    public static void EndPlayerTurn()
    {
        curr_phase = GamePhase.PlayerAnimation;   
    }

    // Interrupt the player turn to move to the player error animation phase
    public static void PlayerError()
    {
        curr_phase = GamePhase.PlayerError;
    }

    // End the player animation phase and move to the player turn phase
    public static void EndPlayerAnimation()
    {
        curr_phase = GamePhase.PlayerResult;
    }

    public static void EndEnemyTurn()
    {
        curr_phase = GamePhase.EnemyAnimation;
    }

    public static void EndEnemyAnimation()
    {
        curr_phase = GamePhase.PlayerTurn;
    }

    // End the player error phase and return to the player move phase
    public static void EndPlayerError()
    {
        curr_phase = GamePhase.PlayerResult;
    }

    // End the player result phase and return to the player move phase
    public static void EndPlayerResult()
    {
        if (!pause_pPlayer)
        {
            // Test for victory
            if(!TestVictory())
            {
                curr_phase = GamePhase.EnemyTurn;
            }
        }
        else
        {
            message_panel.planeDistance = 0.5f;
        }
    }

    // Return the current game phase for testing from any object
    public static GamePhase GetCurrPhase()
    {
        if (!paused)
        {
            return curr_phase;
        }
        else
        {
            return GamePhase.GamePaused;
        }
    }

    public static bool TestVictory()
    {
        if (MarkerControl.LevelComplete())
        {
            // Set game to victory phase
            curr_phase = GamePhase.GameVictory;

            // Get the victory canvas
            win_panel = GameObject.Find("VictoryPanel").GetComponent<Canvas>();

            // Make the win panel visible
            win_panel.planeDistance = 0.5f;

            // If this level has not been completed before
            if (SceneSelectCameraControl.levels_complete < GameObject.Find("BoardBuilder").GetComponent<LevelBase>().Level_id)
            {
                SceneSelectCameraControl.levels_complete = GameObject.Find("BoardBuilder").GetComponent<LevelBase>().Level_id;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public static void LoseGame()
    {
        // Set game to lose phase
        curr_phase = GamePhase.GameLose;

        // Get the loss canvas
        lose_panel = GameObject.Find("LosePanel").GetComponent<Canvas>();

        // Make the loss panel visible
        lose_panel.planeDistance = 0.5f;
    }

    // Reset the game and hide any panels
    public static void ResetPhaseState()
    {
        // Unpause the game
        paused = false;

        // Set the phase so player can move
        curr_phase = GamePhase.PlayerTurn;

        // Hide any panels
        lose_panel.planeDistance = -0.5f;
        win_panel.planeDistance = -0.5f;
        menu_panel.planeDistance = -0.5f;
        message_panel.planeDistance = -0.5f;
    }

    static void PauseGame()
    {
        paused = true;
        menu_panel.planeDistance = 0.5f;
    }

    static void UnpauseGame()
    {
        paused = false;
        menu_panel.planeDistance = -0.5f;
    }

    public void TogglePause()
    {
        if (paused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Go to scene select
    public void GoToSceneSelect()
    {
        SceneManager.LoadScene("SceneSelect_Area1");
    }


    // Go to main menu
    public void GoToSceneMenuMain()
    {
        SceneManager.LoadScene("Menu_Main");
    }

    // Go to next level
    public void GoToSceneNext()
    {
        if (GameObject.Find("BoardBuilder").GetComponent<LevelBase>().next_level != "")
        {
            SceneManager.LoadScene(GameObject.Find("BoardBuilder").GetComponent<LevelBase>().next_level);
        }
    }

    // Set the phases so they will pause after the player's turn
    public static void SetPlayerPause()
    {
        pause_pPlayer = true;
    }

    // Force end the player phase following a message pause
    static void ForceEndPlayerPause()
    {
        pause_pPlayer = false;
        message_panel.planeDistance = -0.5f;
        CashControl.AddPlayCash();
        EndPlayerResult();
    }

    // Set the phases so they will pause after the enemy's turn
    public static void SetEnemyPause()
    {
        pause_pEnemy = true;
    }

    // Force end the enemy phase following a message pause
    static void ForceEndEnemyPause()
    {
        pause_pEnemy = false;
        message_panel.planeDistance = -0.5f;
    }

    // Set the phases so they will pause after the ally's turn
    public static void SetAllyPause()
    {
        pause_pAlly = true;
    }

    // Force end the ally phase following a message pause
    static void ForceEndAllyPause()
    {
        pause_pAlly = false;
        message_panel.planeDistance = -0.5f;
    }

    // Set the phases so they will pause after the environment's turn
    public static void SetEnviroPause()
    {
        pause_pEnvironment = true;
    }

    // Force end the environment phase following a message pause
    static void ForceEndEnviroPause()
    {
        pause_pEnvironment = false;
        message_panel.planeDistance = -0.5f;
    }

    public void EndMessagePause()
    {
        switch (curr_phase)
        {
            case GamePhase.PlayerResult:
                ForceEndPlayerPause();
                break;
            case GamePhase.AllyResult:
                ForceEndAllyPause();
                break;
            case GamePhase.EnemyResult:
                ForceEndEnemyPause();
                break;
            case GamePhase.EnvironmentResult:
                ForceEndEnviroPause();
                break;
        }
    }
}

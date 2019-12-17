using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GamePhase { PlayerTurn, PlayerAnimation, PlayerError, PlayerResult, AllyTurn, AllyAnimation, AllyResult, EnemyTurn, EnemyAnimation, EnemyResult, EnvironmentTurn, EnvironmentResult, TimedTestPhase, GameLose, GameVictory, GamePaused }

public class PhaseController : MonoBehaviour {

    public static GamePhase curr_phase;
    [SerializeField]
    float test_timer;
    static bool paused = false;
    static float time_elapsed_test = 0;
    static Canvas lose_panel, win_panel, menu_panel, message_panel;
    static bool pause_pPlayer, pause_pEnemy, pause_pAlly, pause_pEnvironment;
	static Image turn_icon;
	static Image turn_frame_back;
	static Sprite curr_icon;
	static Color curr_color;

    // Use this for initialization
    void Start () {
		turn_icon = GameObject.Find("TurnStateIcon").GetComponent<Image>();
		turn_frame_back = GameObject.Find("TurnStateFrameBacking").GetComponent<Image>();

        // Initial phase is player turn
        curr_phase = GamePhase.PlayerTurn;

		turn_icon.sprite = InterfaceSingleton.singleton.player_turn_icon;
		turn_frame_back.color = Constants.TURN_COLOR_PLAYER;

		curr_icon = turn_icon.sprite;
		curr_color = turn_frame_back.color;

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

        if ((message_panel.planeDistance == 0.5f) && (Input.GetKeyDown(KeyCode.Space)))
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
        curr_phase = GamePhase.EnvironmentTurn;
    }

	public static void EndAllyTurn()
	{
		curr_phase = GamePhase.AllyAnimation;
	}

	public static void EndAllyAnimation()
	{
		SwitchNetworkController.UpdateAllSwitches();

		curr_phase = GamePhase.EnemyTurn;

		turn_icon.sprite = InterfaceSingleton.singleton.enemy_turn_icon;
		turn_frame_back.color = Constants.TURN_COLOR_ENEMY;

		curr_icon = turn_icon.sprite;
		curr_color = turn_frame_back.color;
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
                curr_phase = GamePhase.AllyTurn;

				turn_icon.sprite = InterfaceSingleton.singleton.ally_turn_icon;
				turn_frame_back.color = Constants.TURN_COLOR_ALLY;

				curr_icon = turn_icon.sprite;
				curr_color = turn_frame_back.color;

				// Deduct light cost
				LightResourceControl.DeductLightCost();

				SwitchNetworkController.UpdateAllSwitches();
			}
        }
        else
        {
            message_panel.planeDistance = 0.5f;
        }
    }

	public static void EndEnvironmentTurn()
	{
		curr_phase = GamePhase.EnvironmentResult;
	}

	public static void EndEnvironmentResult()
	{
		curr_phase = GamePhase.PlayerTurn;

		turn_icon.sprite = InterfaceSingleton.singleton.player_turn_icon;
		turn_frame_back.color = Constants.TURN_COLOR_PLAYER;

		curr_icon = turn_icon.sprite;
		curr_color = turn_frame_back.color;

		LightResourceControl.SetLightDefault();
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

			CashControl.GetLevelEndCash(1f);

			win_panel.transform.Find("Panel/WinCollectText").GetComponent<Text>().text = CashControl.GetPlayCash().ToString();
			win_panel.transform.Find("Panel/WinTurnsMultText").GetComponent<Text>().text = "1.0";
			win_panel.transform.Find("Panel/WinTimeMultText").GetComponent<Text>().text = "1.0";
			win_panel.transform.Find("Panel/WinEnemMultText").GetComponent<Text>().text = "1.0";
			win_panel.transform.Find("Panel/WinTotalText").GetComponent<Text>().text = CashControl.GetLevelCash().ToString();

			CashControl.StoreLevelEndCash();

			// If this level has not been completed before
			GameSave.loaded_save.CompleteLevel(GameObject.Find("BoardBuilder").GetComponent<LevelBase>().Level_id);

			MasterGameController.SaveCurrent();

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

		CashControl.GetLevelEndCash(0.15f);

		lose_panel.transform.Find("Panel/LoseCollectText").GetComponent<Text>().text = CashControl.GetPlayCash().ToString();
		lose_panel.transform.Find("Panel/LoseTotalText").GetComponent<Text>().text = CashControl.GetLevelCash().ToString();

		CashControl.StoreLevelEndCash();
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

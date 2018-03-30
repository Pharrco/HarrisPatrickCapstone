using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    static Text health_text;
	static Image health_fill;
    static int health_max;
    static int health_curr;

    // Use this for initialization
    void Start() {

		health_max = Constants.BASE_HEALTH + GameSave.loaded_save.Player_upgradeState.GetHealthMaxPlusCount();

        // Set initial health to max
        health_curr = health_max;

        // Get Text Field
        health_text = GameObject.Find("HealthText").GetComponent<Text>();

		// Get Fill Image
		health_fill = GameObject.Find("HealthFill").GetComponent<Image>();

		// Update health text
		UpdateHealthText();

		// Update health fill
		UpdateHealthFill();
    }

    // Take damage due to an error
    public static void PlayerTakeDamage()
    {
        // Decrement health
        health_curr = Mathf.Max(0, health_curr - 1);

        // Update health text
        UpdateHealthText();

		// Update health fill
		UpdateHealthFill();

		// Test health loss
		TestHealthLoss();
    }

    // Standardized update to text object
    static void UpdateHealthText()
    {
        // Update health text string
        health_text.text = " Health: " + health_curr + " / " + health_max;
    }

	static void UpdateHealthFill()
	{
		// Update the fill of the health bar
		health_fill.fillAmount = (float)health_curr / (float)health_max;
	}

    // Test whether the player is out of health
    public static void TestHealthLoss()
    {
        if (health_curr <= 0)
        {
            // Lose the game
            PhaseController.LoseGame();

            Debug.Log("Game Over!");
        }
    }
}

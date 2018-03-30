using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LightStatus { Nopwr = 0, White, Infrd, Ulvlt, Dark };

public class LightResourceControl : MonoBehaviour {

    static int light_resource_start;
    static int light_resource_level;
    static int light_cost_white = 5;
    static int light_cost_infrd = 2;
    static int light_cost_ulvlt = 15;
    static int light_cost_nopwr = 0;
    static int light_range_white = 1;
    static int light_range_infrd = 1;
    static int light_range_ulvlt = 2;
    static int light_range_nopwr = 3;
	static Image light_HUD_white, light_HUD_infrd, light_HUD_ulvlt;
	static Color inactive_light_icon = new Color(124, 124, 124, 176);


	public static LightStatus Player_LightStatus { get; private set; }

	// Use this for initialization
	void Start () {
		light_resource_start = Constants.BASE_LIGHT + (Constants.UPGRADEINCREMENT_LIGHT * GameSave.loaded_save.Player_upgradeState.GetPowerMaxPlusCount());

		// Set the light resource to its initial value
		light_resource_level = light_resource_start;

        // Set the initial default light status
        Player_LightStatus = LightStatus.White;

		// Get the light color HUDs
		light_HUD_white = GameObject.Find("WhiteLightHUDIcon").GetComponent<Image>();
		light_HUD_infrd = GameObject.Find("InfrdLightHUDIcon").GetComponent<Image>();
		light_HUD_ulvlt = GameObject.Find("UlvltLightHUDIcon").GetComponent<Image>();
	}

    // Set the light level, testing if the remaining power is sufficient
    public static void SetLightStatus(LightStatus n_status)
    {
        switch(n_status)
        {
            case LightStatus.White:
                // If the player has enough light resource
                if (light_resource_level >= light_cost_white)
                {
                    // Set to white
                    Player_LightStatus = LightStatus.White;
                }
                // Otherwise
                else
                {
                    // Set to default
                    SetLightDefault();
                }
                break;
            case LightStatus.Infrd:
                // If the player has enough light resource
                if (light_resource_level >= light_cost_infrd)
                {
                    // Set to infrared
                    Player_LightStatus = LightStatus.Infrd;
                }
                // Otherwise
                else
                {
                    // Set to default
                    SetLightDefault();
                }
                break;
            case LightStatus.Ulvlt:
                // If the player has enough light resource
                if (light_resource_level >= light_cost_ulvlt)
                {
                    // Set to ultraviolet
                    Player_LightStatus = LightStatus.Ulvlt;
                }
                // Otherwise
                else
                {
                    // Set to default
                    SetLightDefault();
                }
                break;
            case LightStatus.Nopwr:
                // Set to no light
                Player_LightStatus = LightStatus.Nopwr;
                break;
        }
    }

    public static void SetLightDefault()
    {

		// Check for insufficient for ultraviolet
		if ( (Player_LightStatus == LightStatus.Ulvlt) && (light_resource_level < light_cost_ulvlt) && (light_resource_level >= light_cost_white))
		{
			// Set the default to white
			Player_LightStatus = LightStatus.White;
		}
		// Check for insufficient for white
		if (((Player_LightStatus == LightStatus.Ulvlt) || (Player_LightStatus == LightStatus.White)) && (light_resource_level < light_cost_white) && (light_resource_level >= light_cost_infrd))
		{
			// Set the default to infrared
			Player_LightStatus = LightStatus.Infrd;
		}
		// Check for insufficient for infrared
		if (light_resource_level < light_cost_infrd)
        {
            // Set the default to no power
            Player_LightStatus = LightStatus.Nopwr;
        }

		UpdateHUD_Active();
    }

    public static void DeductLightCost()
    {
        switch (Player_LightStatus)
        {
            case LightStatus.White:
                light_resource_level -= light_cost_white;
                break;
            case LightStatus.Infrd:
                light_resource_level -= light_cost_infrd;
                break;
            case LightStatus.Ulvlt:
                light_resource_level -= light_cost_ulvlt;
                break;
            case LightStatus.Nopwr:
                light_resource_level -= light_cost_nopwr;
                break;
        }

		UpdateHUD_Meter();
    }

    //public static void ExecuteLightAction()
    //{
    //    switch (Player_LightStatus)
    //    {
    //        case LightStatus.White:

    //            break;
    //        case LightStatus.Infrd:

    //            break;
    //        case LightStatus.Ulvlt:

    //            break;
    //        case LightStatus.Nopwr:

    //            break;
    //    }
    //}

    public static int GetLightRange()
    {
        switch (Player_LightStatus)
        {
            case LightStatus.White:
                return light_range_white;
            case LightStatus.Infrd:
                return light_range_infrd;
            case LightStatus.Ulvlt:
                return light_range_ulvlt;
            case LightStatus.Nopwr:
                return light_range_nopwr;
            default:
                return light_range_nopwr;
        }
    }

	// Update the light meter on the HUD
	public static void UpdateHUD_Meter()
	{
		GameObject.Find("LightFill").GetComponent<Image>().fillAmount = (float)light_resource_level / (float)light_resource_start;
		GameObject.Find("LightText").GetComponent<Text>().text = Mathf.FloorToInt(100f * (float)light_resource_level / (float)light_resource_start).ToString() + "%";
	}

	public static void UpdateHUD_Active()
	{
		switch (Player_LightStatus)
		{
			case LightStatus.White:
				light_HUD_white.color = Color.white;
				light_HUD_infrd.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_ulvlt.color = Constants.INACTIVE_LIGHT_COLOR;
				break;
			case LightStatus.Ulvlt:
				light_HUD_white.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_infrd.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_ulvlt.color = Color.white;
				break;
			case LightStatus.Infrd:
				light_HUD_white.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_infrd.color = Color.white;
				light_HUD_ulvlt.color = Constants.INACTIVE_LIGHT_COLOR;
				break;
			case LightStatus.Nopwr:
				light_HUD_white.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_infrd.color = Constants.INACTIVE_LIGHT_COLOR;
				light_HUD_ulvlt.color = Constants.INACTIVE_LIGHT_COLOR;
				break;
		}
	}

	// Reset the light resource
	public static void ResetLightResource()
	{
		// Set the light resource to its initial value
		light_resource_level = light_resource_start;

		// Set the initial default light status
		Player_LightStatus = LightStatus.White;

		// Update the HUD
		UpdateHUD_Meter();
		UpdateHUD_Active();
	}

	public void Update()
	{
		// If the current game phase is the player's turn
		if (PhaseController.GetCurrPhase() == GamePhase.PlayerTurn)
		{
			// On key press Alpha # 6
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				SetLightStatus(LightStatus.White);
			}

			// On key press Alpha # 7
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				SetLightStatus(LightStatus.Infrd);
			}

			// On key press Alpha # 8
			if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				SetLightStatus(LightStatus.Ulvlt);
			}

			UpdateHUD_Active();
		}
	}
}

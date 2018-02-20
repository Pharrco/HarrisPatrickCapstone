using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightStatus { Nopwr = 0, White, Infrd, Ulvlt };

public class LightResourceControl : MonoBehaviour {

    static int light_resource_start = 100;
    static int light_resource_level;
    static int light_cost_white = 5;
    static int light_cost_infrd = 2;
    static int light_cost_ulvlt = 15;
    static int light_cost_nopwr = 0;
    static int light_range_white = 1;
    static int light_range_infrd = 1;
    static int light_range_ulvlt = 2;
    static int light_range_nopwr = 3;

    public static LightStatus Player_LightStatus { get; private set; }

	// Use this for initialization
	void Start () {
        // Set the light resource to its initial value
        light_resource_level = light_resource_start;

        // Set the initial default light status
        Player_LightStatus = LightStatus.White;
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
        // If the player has enough light remaining that they can use white
        if (light_resource_level >= light_cost_white)
        {
            // Set the default to white
            Player_LightStatus = LightStatus.White;
        }
        // If the player has enough light remaining that they can use infrared, but not enough to use white
        else if (light_resource_level >= light_cost_infrd)
        {
            // Set the default to infrared
            Player_LightStatus = LightStatus.Infrd;
        }
        else
        {
            // Set the default to no power
            Player_LightStatus = LightStatus.Nopwr;
        }
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
    }

    public static void ExecuteLightAction()
    {
        switch (Player_LightStatus)
        {
            case LightStatus.White:

                break;
            case LightStatus.Infrd:

                break;
            case LightStatus.Ulvlt:

                break;
            case LightStatus.Nopwr:

                break;
        }
    }

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
}

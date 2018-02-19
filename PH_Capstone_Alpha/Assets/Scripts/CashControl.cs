using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashControl : MonoBehaviour {

    static int total_cash, level_cash, play_cash;
    static bool first_set = false;

	// Use this for initialization
	void Start () {
        // If this is the first time this script has been triggered
		if (!first_set)
        {
            // Start with nothing
            total_cash = 0;
            first_set = true;
        }

        // Reset temporary cash amounts
        level_cash = 0;
        play_cash = 0;
	}

    // Get cash in level, add to temporary stock
    static public void AddPlayCash(int add_amount)
    {
        // Add the provided amount to the current cash stock
        play_cash += add_amount;
    }

    public static int GetTotalCash()
    {
        return total_cash;
    }

    public static void GetLevelEndCash()
    {

    }

    public static void StoreLevelEndCash()
    {
        total_cash += level_cash;
    }
}

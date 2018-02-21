using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashControl : MonoBehaviour {

    static int total_cash, level_cash, play_cash, new_cash;
    static bool first_set = false;
    static Text cash_text;

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
        cash_text = GameObject.Find("HUD").GetComponent<Transform>().Find("CashText").GetComponent<Text>();
        cash_text.text = "$" + play_cash;
	}

    // Get cash in level, add to temporary stock
    static public void AddPlayCash(int add_amount)
    {
        // Add the provided amount to the current cash stock
        play_cash += add_amount;

        cash_text.text = "$" + play_cash;
    }

    static public void AddPlayCash()
    {
        // Add the currently stored amount to the current cash stock
        play_cash += new_cash;

        cash_text.text = "$" + play_cash;

        new_cash = 0;
    }

    public static int GetTotalCash()
    {
        return total_cash;
    }

    public static int GetNewCash()
    {
        return new_cash;
    }

    public static int GetPlayCash()
    {
        return play_cash;
    }

    public static void GetLevelEndCash()
    {

    }

    public static void StoreLevelEndCash()
    {
        total_cash += level_cash;
    }

    public static void GenerateRandomCash(int min_amount, int max_amount)
    {
        int rand_amount = Random.Range(min_amount, max_amount);
        new_cash = rand_amount;
    }

    public static void GenerateExactCash(int n_amount)
    {
        new_cash = n_amount;
    }
}

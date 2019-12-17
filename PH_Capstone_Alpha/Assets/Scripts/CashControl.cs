using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashControl : MonoBehaviour {

    static int level_cash, play_cash, new_cash;
    static bool first_set = false;
    static Text cash_text;

	// Use this for initialization
	void Start () {
        // Reset temporary cash amounts
        level_cash = 0;
        play_cash = 0;
        cash_text = GameObject.Find("PlayHUD").GetComponent<Transform>().Find("CashText").GetComponent<Text>();
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

    public static int GetNewCash()
    {
        return new_cash;
    }

    public static int GetPlayCash()
    {
        return play_cash;
    }

	public static int GetLevelCash()
	{
		return level_cash;
	}

    public static void GetLevelEndCash(float multiplier)
    {
		level_cash = Mathf.FloorToInt((float)play_cash * multiplier);
    }

    public static void StoreLevelEndCash()
    {
		GameSave.loaded_save.AddPlayerCash(level_cash);
		GameSave.UpdateAchievement(AchievementType.GoldCollect, level_cash);
		GameSave.UpdateAchievement(AchievementType.GoldHoard, GameSave.loaded_save.GetPlayerCash());
		Debug.Log("Player now has $" + GameSave.loaded_save.GetPlayerCash().ToString());
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

	// Reset cash
	public static void ResetLevelCash()
	{
		// Reset temporary cash amounts
		level_cash = 0;
		play_cash = 0;
		cash_text.text = "$" + play_cash;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
	public static GameSave loaded_save;
	private int player_cash;
	private int levels_complete;
	public UpgradeState Player_upgradeState { get; protected set; }
	public AchieveState Player_achieveState { get; protected set; }

	public GameSave(int start_cash = 0)
	{
		player_cash = start_cash;
		levels_complete = 0;
		Player_upgradeState = new UpgradeState();
		Player_achieveState = new AchieveState();
	}

	// Add to player's cash total
	public void AddPlayerCash(int add_cash)
	{
		player_cash += add_cash;
		GameSave.UpdateAchievement(AchievementType.GoldHoard, GameSave.loaded_save.GetPlayerCash());
		MasterGameController.SaveCurrent();
	}

	// Return the player's total cash
	public int GetPlayerCash()
	{
		return player_cash;
	}

	// Spend cash
	public bool SpendPlayerCash(int spend_cash)
	{
		// If the player has enough cash
		if (spend_cash <= player_cash)
		{
			// Remove the cash and return successful spend
			player_cash -= spend_cash;
			GameSave.UpdateAchievement(AchievementType.GoldHoard, GameSave.loaded_save.GetPlayerCash());
			return true;
		}

		// Insufficient funds, return unsuccessful
		return false;
	}

	// Get levels complete
	public int GetLevelProgress()
	{
		return levels_complete;
	}

	// A level has been completed, unlock next level
	public void CompleteLevel(int completed_level)
	{
		levels_complete = Mathf.Max(completed_level, levels_complete);
	}

	public static void UpdateAchievement(AchievementType n_type, int n_count)
	{
		loaded_save.Player_achieveState.AchievementDict[n_type].UpdateAchievementSuccess(n_count);

		MasterGameController.SaveCurrent();
	}
}
